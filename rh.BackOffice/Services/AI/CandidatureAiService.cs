using Microsoft.EntityFrameworkCore;
using rh.BackOffice.Services.Cv;
using rh.Infrastructure.Data;

namespace rh.BackOffice.Services.AI
{
    public class CandidatureAiService
    {
        private readonly AppDbContext _context;
        private readonly EmbeddingService _embeddingService;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<CandidatureAiService> _logger;
        public CandidatureAiService(
            AppDbContext context,
            EmbeddingService embeddingService,
            IWebHostEnvironment env,
            ILogger<CandidatureAiService> logger)
        {
            _context = context;
            _embeddingService = embeddingService;
            _env = env;
            _logger = logger;
        }

        public async Task AnalyserAnnonceAsync(int annonceId)
        {
            _logger.LogInformation("🔍 Début de l'analyse pour l'annonce ID: {AnnonceId}", annonceId);

            var annonce = await _context.Annonces
                .Include(a => a.Candidatures)
                    .ThenInclude(c => c.Candidat)
                .FirstOrDefaultAsync(a => a.Id == annonceId);

            if (annonce == null)
            {
                _logger.LogWarning("⚠️ Annonce ID {AnnonceId} introuvable", annonceId);
                return;
            }

            _logger.LogInformation("✅ Annonce '{Libelle}' trouvée avec {Count} candidature(s)",
                annonce.Libelle, annonce.Candidatures?.Count ?? 0);

            // 1️⃣ Construire le texte de référence (annonce)
            string texteAnnonce =
                $"{annonce.Libelle}\n{annonce.Description}\n{annonce.CompetenceRequis}\n" +
                $"{annonce.NiveauExperience}\n{annonce.Localisation}";

            _logger.LogInformation("📄 Texte de l'annonce construit ({Length} caractères)", texteAnnonce.Length);
            _logger.LogDebug("Texte annonce: {Texte}", texteAnnonce.Substring(0, Math.Min(200, texteAnnonce.Length)));

            _logger.LogInformation("🔢 Génération du vecteur pour l'annonce...");
            var annonceVector = await _embeddingService.GetEmbeddingAsync(texteAnnonce);
            _logger.LogInformation("✅ Vecteur annonce généré: {Dimensions} dimensions", annonceVector.Length);

            // 2️⃣ Analyser chaque CV
            int candidaturesAnalysees = 0;
            int candidaturesIgnorees = 0;

            foreach (var candidature in annonce.Candidatures)
            {
                _logger.LogInformation("--- Analyse candidature ID: {CandidatureId} ---", candidature.Id);

                if (string.IsNullOrEmpty(candidature.Candidat?.PieceJointe))
                {
                    _logger.LogWarning("⏭️ Candidature {CandidatureId} ignorée: pas de CV attaché", candidature.Id);
                    candidaturesIgnorees++;
                    continue;
                }

                string cvPath = Path.Combine(_env.WebRootPath, "cvs", candidature.Candidat.PieceJointe);
                _logger.LogInformation("📁 Chemin du CV: {CvPath}", cvPath);

                if (!File.Exists(cvPath))
                {
                    _logger.LogWarning("⏭️ Candidature {CandidatureId} ignorée: fichier CV introuvable à {Path}",
                        candidature.Id, cvPath);
                    candidaturesIgnorees++;
                    continue;
                }

                _logger.LogInformation("📖 Extraction du texte du CV...");
                string texteCv = CvTextExtractor.ExtractText(cvPath);
                _logger.LogInformation("✅ Texte extrait: {Length} caractères", texteCv.Length);
                _logger.LogDebug("Extrait CV: {Texte}", texteCv.Substring(0, Math.Min(200, texteCv.Length)));

                _logger.LogInformation("🔢 Génération du vecteur pour le CV...");
                var cvVector = await _embeddingService.GetEmbeddingAsync(texteCv);
                _logger.LogInformation("✅ Vecteur CV généré: {Dimensions} dimensions", cvVector.Length);

                // 3️⃣ Calcul du score
                _logger.LogInformation("🧮 Calcul de la similarité...");
                double score = VectorUtils.CosineSimilarity(annonceVector, cvVector);
                candidature.ScoreCorrespondance = Math.Round(score, 3);

                _logger.LogInformation("🎯 Score calculé pour candidature {CandidatureId}: {Score}",
                    candidature.Id, candidature.ScoreCorrespondance);

                candidaturesAnalysees++;
            }

            _logger.LogInformation("💾 Sauvegarde des scores en base de données...");
            await _context.SaveChangesAsync();
            _logger.LogInformation("✅ Scores sauvegardés avec succès");

            _logger.LogInformation("📊 Résumé: {Analysees} candidature(s) analysée(s), {Ignorees} ignorée(s)",
                candidaturesAnalysees, candidaturesIgnorees);
        }
    }
}

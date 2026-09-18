using System;
using System.Collections.Generic;
using System.IO;
using JatekLogika;
using NUnit.Framework;

namespace JatekLogika.Tests
{
    public class PlayerCsvLoaderTests
    {
        private string? _tempFile;

        [TearDown]
        public void TearDown()
        {
            if (_tempFile != null && File.Exists(_tempFile))
            {
                File.Delete(_tempFile);
            }
        }

        [Test]
        public void LoadCandidates_SkipsHeaderAndParsesRemainingLines()
        {
            _tempFile = Path.GetTempFileName();
            File.WriteAllLines(_tempFile, new[]
            {
                "id;name",
                "1;Anita Job",
                "2;Barry Cade"
            });

            List<PlayerCandidate> candidates = PlayerCsvLoader.LoadCandidates(_tempFile);

            Assert.That(candidates, Has.Count.EqualTo(2));
            Assert.That(candidates[0].Id, Is.EqualTo(1));
            Assert.That(candidates[0].Name, Is.EqualTo("Anita Job"));
            Assert.That(candidates[1].Id, Is.EqualTo(2));
            Assert.That(candidates[1].Name, Is.EqualTo("Barry Cade"));
        }

        [Test]
        public void LoadCandidates_IgnoresEmptyLines()
        {
            _tempFile = Path.GetTempFileName();
            File.WriteAllLines(_tempFile, new[]
            {
                "id;name",
                "1;Anita Job",
                "",
                "2;Barry Cade"
            });

            List<PlayerCandidate> candidates = PlayerCsvLoader.LoadCandidates(_tempFile);

            Assert.That(candidates, Has.Count.EqualTo(2));
        }

        [Test]
        public void LoadCandidates_MissingFile_ThrowsFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(
                () => PlayerCsvLoader.LoadCandidates("nem-letezo-fajl.csv"));
        }
    }
}

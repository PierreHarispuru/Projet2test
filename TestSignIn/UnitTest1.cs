using Projet2.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestSignIn
{
    public class UnitTest1
    {
        [Fact]
        public void Creation_Compte()
        {
            // Nous supprimons la base si elle existe puis nous la créons
            using (Dal dal = new Dal())
            {
                // Nous supprimons et créons la db avant le test
                dal.DeleteCreateDatabase();
                // Nous créons un lieu de vacances
                dal.CreerSejour("Chambord", "1111111111");

                // Nous vérifions que le lieu a bien été créé
                List<Sejour> sejours = dal.ObtientTousLesSejours();
                Assert.NotNull(sejours);
                Assert.Single(sejours);
                Assert.Equal("Chambord", sejours[0].Lieu);
                Assert.Equal("1111111111", sejours[0].Telephone);
            }
        }
    }
}

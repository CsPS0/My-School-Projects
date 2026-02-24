using passLib;

namespace solticsongor_JelszoTeszt;

public class PasswordTests
{
    [Test]
    public void HelyesJelszo_JoBemenet_True()
    {
        string pwd = "Password123";
        bool result = Password.IsValid(pwd);
        Assert.That(result, Is.True, "A jelszónak érvényesnek kellene lennie.");
    }

    [Test]
    public void Helytelen_NincsKisbetu_False()
    {
        string pwd = "PASSWORD123";
        bool result = Password.IsValid(pwd);
        Assert.That(result, Is.False, "Hiba: Hiányzó kisbetű esetén False-t kellene adnia.");
    }

    [Test]
    public void Helytelen_NincsNagybetu_False()
    {
        string pwd = "password123";
        bool result = Password.IsValid(pwd);
        Assert.That(result, Is.False, "Hiba: Hiányzó nagybetű esetén False-t kellene adnia.");
    }

    [Test]
    public void Helytelen_NincsSzam_False()
    {
        string pwd = "PasswordTest";
        bool result = Password.IsValid(pwd);
        Assert.That(result, Is.False, "Hiba: Hiányzó szám esetén False-t kellene adnia.");
    }

    [Test]
    public void Helytelen_Rovid_False()
    {
        string pwd = "Pass1";
        bool result = Password.IsValid(pwd);
        Assert.That(result, Is.False, "Hiba: Túl rövid jelszó esetén False-t kellene adnia.");
    }

    
    [Test]
    public void EdgeCase_CsakEkezetesKisbetu_True()
    {
        string pwd = "ÁRVÍZTŰRŐé123";
        bool result = Password.IsValid(pwd);
        Assert.That(result, Is.True, "Az ékezetes kisbetűt is el kellene fogadnia.");
    }

    [Test]
    public void EdgeCase_CsakEkezetesNagybetu_True()
    {
        string pwd = "árvíztűrőÉ123";
        bool result = Password.IsValid(pwd);
        Assert.That(result, Is.True, "Az ékezetes nagybetűt is el kellene fogadnia.");
    }

    [Test]
    public void EdgeCase_UnicodeHossz_False()
    {
        string pwd = "A1a" + "\uD83D\uDE00";
        
        bool result = Password.IsValid(pwd);
        Assert.That(result, Is.False, "A karakterszám alapján (ami < 6) érvénytelennek kell lennie.");
    }
}
using ActivationCodeValidator_lib;
using NUnit.Framework;

namespace Test;

[TestFixture]
public class Tests
{
    [Test]
    public void IsItTheGoodCode()
    {
        Assert.That(GoodLogic.CheckCodeGood("KGCW-5881-JUAH"), Is.True, "The code must follow the rules!");
        Assert.That(GoodLogic.CheckCodeGood("CAJAVALK88GK"), Is.True, "The code must follow the rules!");
    }

    [Test]
    public void IsItBadCode()
    {
        Assert.That(BadLogic.CheckCodeBad(""), Is.False, "The code shouldn't follow the rules! | Empty");
        Assert.That(BadLogic.CheckCodeBad("AAAK-BBBB-JCG88"), Is.False, "The code should've failed | Too long");
        Assert.That(BadLogic.CheckCodeBad("AAAA-BBBB-CCCC"), Is.False, "This code should've failed | Missing required characters");
        Assert.That(BadLogic.CheckCodeBad("--GGJJCCCC1234"), Is.False, "This code should've failed | Wrong hyphen position");
        Assert.That(BadLogic.CheckCodeBad("gkjj-aaaa-8834"), Is.False, "This code should've failed | Too many A letters");
        Assert.That(BadLogic.CheckCodeBad("AA-DA-GC-KK-88-T0"), Is.False, "This code should've failed | Too many hyphens");
        Assert.That(BadLogic.CheckCodeBad("CAJAVA88G"), Is.False, "This code should've failed | Too short");
        Assert.That(BadLogic.CheckCodeBad("JACA-U88G-KMOP1"), Is.False, "This code should've failed | Too long");
        Assert.That(BadLogic.CheckCodeBad("KACA-UU1G888OP1"), Is.False, "This code should've failed | Too many 8s");
        Assert.That(BadLogic.CheckCodeBad("ÖÜÓŐ-6767-ÉÁŰÚ"), Is.False, "This code should've failed | Non-English letters");
        Assert.That(BadLogic.CheckCodeBad("1234-5678-9671"), Is.False, "This code should've failed | Only numbers");
        Assert.That(BadLogic.CheckCodeBad("ABCD-EFGH-IJKL"), Is.False, "This code should've failed | No 88");
        Assert.That(BadLogic.CheckCodeBad("<>~ˇ-^˘°˛-`˙´˝"), Is.False, "This code should've failed | Special symbols");
        Assert.That(BadLogic.CheckCodeBad("A B - 1 2- C D"), Is.False, "This code should've failed | Contains spaces");
    }

    [TestCase("KGCW-5881-JUAH", ExpectedResult = true)]
    [TestCase("kgcw-5881-juah", ExpectedResult = true)]
    [TestCase("Kgcw-5881-Juah", ExpectedResult = true)]
    [TestCase("CAJAVALK88GK", ExpectedResult = true)]
    [TestCase("K123-A88B-CDEF", ExpectedResult = true)]
    [TestCase("KKKK-88BC-DEFG", ExpectedResult = true)]
    public bool TestValidCodes(string code)
    {
        return ActivatetionCodeValidator_lib.CheckCodeValidity(code);
    }

    [TestCase(null, ExpectedResult = false)]
    [TestCase("", ExpectedResult = false)]
    [TestCase("   ", ExpectedResult = false)]
    [TestCase("K123-888B-CDEF", ExpectedResult = false)]
    [TestCase("K183-A88B-CDEF", ExpectedResult = false)]
    [TestCase("K183-A81B-CDEF", ExpectedResult = false)]
    [TestCase("X123-A88B-CDEF", ExpectedResult = false)]
    [TestCase("KAAA-A88B-CDEF", ExpectedResult = false)]
    [TestCase("K123-A88B-CDE!", ExpectedResult = false)]
    [TestCase("K123-A88B-CDE", ExpectedResult = false)]
    [TestCase("K123-A88B-CDEF1", ExpectedResult = false)]
    [TestCase("K123A88BCDE", ExpectedResult = false)]
    [TestCase("K123A88BCDEFG", ExpectedResult = false)]
    public bool TestInvalidBoundaryConditions(string? code)
    {
        return ActivatetionCodeValidator_lib.CheckCodeValidity(code);
    }
}

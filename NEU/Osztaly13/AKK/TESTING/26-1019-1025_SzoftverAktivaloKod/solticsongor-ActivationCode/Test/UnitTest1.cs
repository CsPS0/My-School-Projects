using ActivationCodeValidator_lib;
using Newtonsoft.Json;

namespace Test;

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
        Assert.That(BadLogic.CheckCodeBad(""), Is.False, "The code shouldn't follow the rules!; the code mustn't follow the rules! | The code should be empty");
        Assert.That(BadLogic.CheckCodeBad("AAAK-BBBB-JCG88"), Is.False, "The code should've failed; the code mustn't follow the rules! | The code should be too long");
        Assert.That(BadLogic.CheckCodeBad("AAAA-BBBB-CCCC"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code shouldn't have the required characters");
        Assert.That(BadLogic.CheckCodeBad("--GGJJCCCC1234"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code should have the following in the right position and should have missing characters");
        Assert.That(BadLogic.CheckCodeBad("gkjj-aaaa-8834"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code should have more characters then the required");
        Assert.That(BadLogic.CheckCodeBad("AA-DA-GC-KK-88-T0"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code should have too many following lines");
        Assert.That(BadLogic.CheckCodeBad("CAJAVA88G"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code should have be too short");
        Assert.That(BadLogic.CheckCodeBad("JACA-U88G-KMOP1"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code should have be too long");
        Assert.That(BadLogic.CheckCodeBad("KACA-UU1G888OP1"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code should have bad formating, too many character and too many 8 character");
        Assert.That(BadLogic.CheckCodeBad("ÖÜÓŐ-6767-ÉÁŰÚ"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code should have had letters from the non english alphabet");
        Assert.That(BadLogic.CheckCodeBad("1234-5678-9671"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code should have had only numbers");
        Assert.That(BadLogic.CheckCodeBad("ABCD-EFGH-IJKL"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code should have had only letters");
        Assert.That(BadLogic.CheckCodeBad("<>~ˇ-^˘°˛-`˙´˝"), Is.False, "This code should've failed; the code mustn't follow the rules! | The code should have had special symbols");
        Assert.That(BadLogic.CheckCodeBad("A B - 1 2- C D"), Is.False,  "This code should've failed; the code mustn't follow the rules! | The code should have had empty symbols");
    }
}
using PokerLib;
using Xunit;

namespace PokerTest;

public class HandsTest
{
    [Fact]
    public void NewHandIsEmpty()
    {
        var hand = new Hand();
        Assert.Empty(hand.Cards);
    }

    [Fact]
    public void CanAddCard()
    {
        var hand = new Hand();
        bool result = hand.AddCard(Hand.Faces.Ace, Hand.Suites.Hearts);
        Assert.True(result);
        Assert.Single(hand.Cards);
    }

    [Fact]
    public void CardStoredCorrectly()
    {
        var hand = new Hand();
        hand.AddCard(Hand.Faces.King, Hand.Suites.Spades);
        var card = hand.Cards[0];
        Assert.Equal(Hand.Faces.King, card.Face);
        Assert.Equal(Hand.Suites.Spades, card.Suite);
    }

    [Fact]
    public void CanAddTwoDifferentCards()
    {
        var hand = new Hand();
        Assert.True(hand.AddCard(Hand.Faces.Ace, Hand.Suites.Hearts));
        Assert.True(hand.AddCard(Hand.Faces.Two, Hand.Suites.Clubs));
        Assert.Equal(2, hand.Cards.Count);
    }

    [Fact]
    public void CannotAddDuplicateCard()
    {
        var hand = new Hand();
        hand.AddCard(Hand.Faces.Ace, Hand.Suites.Hearts);
        bool result = hand.AddCard(Hand.Faces.Ace, Hand.Suites.Hearts);
        Assert.False(result);
        Assert.Single(hand.Cards);
    }

    [Fact]
    public void HandCanHaveMax5Cards()
    {
        var hand = new Hand();
        hand.AddCard(Hand.Faces.Ace, Hand.Suites.Hearts);
        hand.AddCard(Hand.Faces.Two, Hand.Suites.Hearts);
        hand.AddCard(Hand.Faces.Three, Hand.Suites.Hearts);
        hand.AddCard(Hand.Faces.Four, Hand.Suites.Hearts);
        hand.AddCard(Hand.Faces.Five, Hand.Suites.Hearts);
        Assert.Equal(5, hand.Cards.Count);
    }

    [Fact]
    public void CannotAddSixthCard()
    {
        var hand = new Hand();
        hand.AddCard(Hand.Faces.Ace, Hand.Suites.Hearts);
        hand.AddCard(Hand.Faces.Two, Hand.Suites.Hearts);
        hand.AddCard(Hand.Faces.Three, Hand.Suites.Hearts);
        hand.AddCard(Hand.Faces.Four, Hand.Suites.Hearts);
        hand.AddCard(Hand.Faces.Five, Hand.Suites.Hearts);
        
        bool result = hand.AddCard(Hand.Faces.Six, Hand.Suites.Hearts);
        Assert.False(result);
        Assert.Equal(5, hand.Cards.Count);
    }

    [Fact]
    public void HandBuiltFromStringCorrectly()
    {
        var hand = new Hand();
        hand.AddHand("HA, D5");
        Assert.Equal(2, hand.Cards.Count);
        Assert.Contains(hand.Cards, c => c.Face == Hand.Faces.Ace && c.Suite == Hand.Suites.Hearts);
        Assert.Contains(hand.Cards, c => c.Face == Hand.Faces.Five && c.Suite == Hand.Suites.Diamonds);
    }

    [Fact]
    public void AllCardsRemoved()
    {
        var hand = new Hand();
        hand.AddCard(Hand.Faces.Ace, Hand.Suites.Hearts);
        hand.RemoveAllCards();
        Assert.Empty(hand.Cards);
    }
}
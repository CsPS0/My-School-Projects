using PokerLib;
using Xunit;
using System.Linq;

namespace PokerTest;

public class PokerTests
{
    [Fact]
    public void CanAddTwoPlayers()
    {
        var poker = new Poker();
        poker.AddPlayers(2);
        Assert.Equal(2, poker.PlayerCount);
    }

    [Fact]
    public void PlayersHave5CardsAfterDeal()
    {
        var poker = new Poker();
        poker.AddPlayers(3);
        poker.Deal();
        foreach (var hand in poker.Hands)
        {
            Assert.Equal(5, hand.Cards.Count);
        }
    }

    [Fact]
    public void RoyalFlushWins()
    {
        var poker = new Poker();
        poker.AddPlayers(2);
        
        poker.Hands[0].RemoveAllCards();
        poker.Hands[0].AddHand("HA, HK, HQ, HJ, H10");
        
        poker.Hands[1].RemoveAllCards();
        poker.Hands[1].AddHand("D2, D3, D4, D5, D7"); 

        int winnerIndex = poker.PickWinner();
        Assert.Equal(0, winnerIndex);
    }

    [Fact]
    public void WinnerSelectionWithMultiplePlayers()
    {
        var poker = new Poker();
        poker.AddPlayers(3);

        poker.Hands[0].RemoveAllCards();
        poker.Hands[0].AddHand("C2, C3, C4, C5, C7");

        poker.Hands[1].RemoveAllCards();
        poker.Hands[1].AddHand("SA, SK, SQ, SJ, S10");

        poker.Hands[2].RemoveAllCards();
        poker.Hands[2].AddHand("H2, H3, H4, H5, H7");

        int winnerIndex = poker.PickWinner();
        Assert.Equal(1, winnerIndex);
    }
}
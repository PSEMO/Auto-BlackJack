using System.Linq.Expressions;

Console.WriteLine("----START----");
List<Card> FullDeck = new List<Card>();

for (int i = 1; i < 14; i++)
{
    string c;
    switch (i)
    {
        case 1:
            c = "A";
            break;
        case 11:
            c = "Q";
            break;
        case 12:
            c = "K";
            break;
        case 13:
            c = "J";
            break;
        default:// i = [2, 10]
            c = (i + "");
            break;
    }

    for (int j = 0; j < 4; j++)
    {
        FullDeck.Add(new Card(c));
    }
}

//---------------------------------------------------------------------------------

Console.WriteLine("Cards in a full deck:");
WriteWholeDecks(FullDeck);

//---------------------------------------------------------------------------------

int GameCount = 0;
int GameWon = 0;
int GameLost = 0;
int GameDraw = 0;

Console.WriteLine("----Game Start----");
while (GameCount < 9999999)//100.000.000
{
    GameCount++;

    List<Card> House = new List<Card>();
    House.Add(RandomCardGiver());
    House.Add(RandomCardGiver());

    //Console.WriteLine("Starting House:");
    //WriteWholeDecks(House);

    while (!(GiveCurrentPoint(House) >= 17 && GiveCurrentPoint(House) <= 21))
    {
        if(GiveCurrentPoint(House) > 21)
        {
            break;
        }
        House.Add(RandomCardGiver());
    }

    //////////////////////////
    int OpenHouseCardPoint = CardToPoint(House[0]);

    List<Card> Player = new List<Card>();
    Player.Add(RandomCardGiver());
    Player.Add(RandomCardGiver());

    //Console.WriteLine("Starting Player:");
    //WriteWholeDecks(Player);

    #region Player:
    while (true)
    {
        if (GiveCurrentPoint(Player) <= 11)
        {
            Player.Add(RandomCardGiver());
        }
        else if (GiveCurrentPoint(Player) == 12)
        {
            if(OpenHouseCardPoint >= 4 && OpenHouseCardPoint <= 6)
            {
                break;
            }
            else
            {
                Player.Add(RandomCardGiver());
            }
        }
        else if (GiveCurrentPoint(Player) == 13)
        {
            if (OpenHouseCardPoint <= 6)
            {
                break;
            }
            else
            {
                Player.Add(RandomCardGiver());
            }
        }
        else if (GiveCurrentPoint(Player) == 14)
        {
            if (OpenHouseCardPoint <= 6)
            {
                break;
            }
            else
            {
                Player.Add(RandomCardGiver());
            }
        }
        else if (GiveCurrentPoint(Player) == 15)
        {
            if (OpenHouseCardPoint <= 6)
            {
                break;
            }
            else
            {
                Player.Add(RandomCardGiver());
            }
        }
        else if (GiveCurrentPoint(Player) == 16)
        {
            if (OpenHouseCardPoint >= 4 && OpenHouseCardPoint <= 6)
            {
                break;
            }
            else
            {
                Player.Add(RandomCardGiver());
            }
        }
        else if (GiveCurrentPoint(Player) >= 17)
        {
            break;
        }
    }
    #endregion

    //Console.WriteLine("End Player:");
    //WriteWholeDecks(Player);

    int playerPoint = GiveCurrentPoint(Player);
    int housePoint = GiveCurrentPoint(House);

    //Console.WriteLine("End House:");
    //WriteWholeDecks(House);

    #region House:
    if (housePoint < 21)
    {
        if(playerPoint < 21)
        {
            if(housePoint > playerPoint)
            {
                GameLost++;
            }
            else if(housePoint < playerPoint)
            {
                GameWon++;
            }
            else
            {
                GameDraw++;
            }
        }
        else
        {
            GameLost++;
        }
    }
    else
    {
        if (playerPoint < 21)
        {
            GameWon++;
        }
        else
        {
            GameLost++;
        }
    }
    #endregion

    //Console.WriteLine("--------------------------");
}

//---------------------------------------------------------------------------------

Console.WriteLine("-----Game End-----" + "\n");
Console.WriteLine("Game count: " + GameCount);
Console.WriteLine("Game won count: " + GameWon + " - " + (((float)GameWon / GameCount) * 100) + "%");
Console.WriteLine("Game lost count: " + GameLost + " - " + (((float)GameLost / GameCount) * 100) + "%");
Console.WriteLine("Game draw count: " + GameDraw + " - " + (((float)GameDraw / GameCount) * 100) + "%"); 

//---------------------------------------------------------------------------------

void WriteWholeDecks(List<Card> cards)
{
    foreach (Card card in cards)
    {
        Console.Write(card.Name + " ");
    }
    Console.WriteLine();
}

int CardToPoint(Card card)
{
    switch (card.Name)
    {
        case "A":
            return 11;
        case "1":
            return 1;
        case "2":
            return 2;
        case "3":
            return 3;
        case "4":
            return 4;
        case "5":
            return 5;
        case "6":
            return 6;
        case "7":
            return 7;
        case "8":
            return 8;
        case "9":
            return 9;
        case "10":
            return 10;
        case "K":
            return 10;
        case "Q":
            return 10;
        case "J":
            return 10;
    }
    return 0;
}

int GiveCurrentPoint(List<Card> cards)
{
    int current = 0;
    int numberOfA = 0;

    foreach (Card card in cards)
    {
        current += CardToPoint(card);
        if (card.Name == "A")
            numberOfA++;
    }

    while (current > 21 && numberOfA > 0)
    {
        current -= 10;
        numberOfA--;
    }

    return current;
}

Card RandomCardGiver()
{
    Random rnd = new Random();
    int rn = rnd.Next(0, FullDeck.Count);

    return FullDeck[rn];
}

//---------------------------------------------------------------------------------

public class Card
{
    //A, 2, 3, 4, 5, 6, 7, 8, 9, 10, Q, K, J
    public string Name = "0";

    public Card(string name)
    {
        Name = name;
    }
}
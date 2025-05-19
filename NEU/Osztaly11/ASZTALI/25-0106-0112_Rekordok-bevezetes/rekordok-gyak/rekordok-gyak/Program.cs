#region 1.feladat
// a.
string[] April = File.ReadAllLines("aprilis.txt");

List<Competition> competitions = new List<Competition>();

for (int i = 0; i < April.Length; i++)
{
    string[] parts = April[i].Split(';');
    string className = parts[0];
    int points = int.Parse(parts[1]);
    competitions.Add(new Competition(className, points));
}

// b.
int max = -1;
string bestClass = "";

for (int i = 0; i < competitions.Count; i++)
{
    if (competitions[i].Point > max)
    {
        max = competitions[i].Point;
        bestClass = competitions[i].Class;
    }
}

Console.WriteLine($"In April the best competetive class is nothing but the {bestClass} with {max} points.");

// c.
double average = 0;
for (int i = 0; i < competitions.Count; i++)
{
    average += competitions[i].Point;
}
average = average / competitions.Count;
Console.WriteLine($"The classes average points are: {average}");

// d.
int under100 = 0;
for (int i = 0; i < competitions.Count; i++)
{
    if (competitions[i].Point < 100)
    {
        under100++;
    }
}
Console.WriteLine($"From the classes the people who got point under the score of 100 are: {under100}.");

// e.
Console.Write("Enter a class name: ");
string searchClassName = Console.ReadLine();
if (string.IsNullOrEmpty(searchClassName))
{
    Console.WriteLine("Class name cannot be empty!");
    return;
}

bool found = false;
for (int i = 0; i < competitions.Count; i++)
{
    if (competitions[i].Class == searchClassName)
    {
        Console.WriteLine($"Points for class {searchClassName}: {competitions[i].Point}");
        found = true;
        break;
    }
}

if (!found)
{
    Console.WriteLine($"Class {searchClassName} not found.");
}

// f.
Console.WriteLine("Classes with less than 50 points:");
for (int i = 0; i < competitions.Count; i++)
{
    if (competitions[i].Point < 50)
    {
        Console.WriteLine($"{competitions[i].Class}: {competitions[i].Point} points");
    }
}
#endregion

#region 2.feladat
// a.
string[] Like = File.ReadAllLines("like.txt");

List<WebPage> webPages = new List<WebPage>();

for (int i = 0; i < Like.Length; i++)
{
    string[] parts = Like[i].Split(';');
    string monogram = parts[0];
    int likeCount = int.Parse(parts[1]);
    webPages.Add(new WebPage(monogram, likeCount));
}

// b.
for (int i = 0; i < webPages.Count; i++)
{
    Console.WriteLine($"Post {i + 1}: {webPages[i].Monogram} - {webPages[i].LikeCount} likes");
}

// c.
int totalLikes = 0;
for (int i = 0; i < webPages.Count; i++)
{
    totalLikes += webPages[i].LikeCount;
}
Console.WriteLine($"Total likes: {totalLikes}");

// d.
double averageLikes = 0;
for (int i = 0; i < webPages.Count; i++)
{
    averageLikes += webPages[i].LikeCount;
}
averageLikes = averageLikes / webPages.Count;
Console.WriteLine($"Average likes per post: {averageLikes}");

//e.
int minLikes = int.MaxValue;
WebPage minPost = null;
for (int i = 0; i < webPages.Count; i++)
{
    if (webPages[i].LikeCount < minLikes)
    {
        minLikes = webPages[i].LikeCount;
        minPost = webPages[i];
    }
}
Console.WriteLine($"Post with the least likes: {webPages.IndexOf(minPost) + 1}, posted by {minPost.Monogram}.");

//f.
int maxLikes = int.MinValue;
WebPage maxPost = null;
for (int i = 0; i < webPages.Count; i++)
{
    if (webPages[i].LikeCount > maxLikes)
    {
        maxLikes = webPages[i].LikeCount;
        maxPost = webPages[i];
    }
}
Console.WriteLine($"Post with the most likes: {webPages.IndexOf(maxPost) + 1}, posted by {maxPost.Monogram}.");

//g.
Console.Write("Enter a like count: ");
string likeInput = Console.ReadLine();
int likeThreshold;

if (string.IsNullOrEmpty(likeInput) || !int.TryParse(likeInput, out likeThreshold))
{
    Console.WriteLine("Invalid input! Please enter a valid number.");
    return;
}

bool foundPost = false;
for (int i = 0; i < webPages.Count; i++)
{
    if (webPages[i].LikeCount >= likeThreshold)
    {
        Console.WriteLine($"Post {i + 1}: {webPages[i].Monogram} - {webPages[i].LikeCount} likes");
        foundPost = true;
    }
}

if (!foundPost)
{
    Console.WriteLine("No post reached this like count.");
}

//h.
Console.WriteLine("Employees who got more than 1500 likes on their posts:");
for (int i = 0; i < webPages.Count; i++)
{
    if (webPages[i].LikeCount > 1500)
    {
        Console.WriteLine($"{webPages[i].Monogram} (Post {i + 1})");
    }
}

//i.
#endregion

#region 3.feladat
// a.
string[] Voting = File.ReadAllLines("alkoto.txt");

List<Contestant> contestants = new List<Contestant>();

for (int i = 0; i < Voting.Length; i += 2)
{
    string name = Voting[i];
    string[] votes = Voting[i + 1].Split(' ');
    int invalidVotes = int.Parse(votes[0]);
    int validVotes = int.Parse(votes[1]);
    contestants.Add(new Contestant(name, invalidVotes, validVotes));
}

// b.
int totalValidVotes = 0;
for (int i = 0; i < contestants.Count; i++)
{
    totalValidVotes += contestants[i].ValidVotes;
}
Console.WriteLine($"Total valid votes: {totalValidVotes}");

// c.
double averageInvalidVotes = 0;
for (int i = 0; i < contestants.Count; i++)
{
    averageInvalidVotes += contestants[i].InvalidVotes;
}
averageInvalidVotes = averageInvalidVotes / contestants.Count;
Console.WriteLine($"Average invalid votes: {averageInvalidVotes}");

// d.
int maxValidVotes = int.MinValue;
Contestant maxValidContestant = null;
for (int i = 0; i < contestants.Count; i++)
{
    if (contestants[i].ValidVotes > maxValidVotes)
    {
        maxValidVotes = contestants[i].ValidVotes;
        maxValidContestant = contestants[i];
    }
}
Console.WriteLine($"Contestant with the most valid votes: {maxValidContestant.Name} with {maxValidContestant.ValidVotes} votes.");

//e.
bool foundInvalidOver10 = false;
string firstInvalidOver10Name = "";

for (int i = 0; i < contestants.Count; i++)
{
    if (contestants[i].InvalidVotes > 10)
    {
        foundInvalidOver10 = true;
        firstInvalidOver10Name = contestants[i].Name;
        break;
    }
}

if (foundInvalidOver10)
{
    Console.WriteLine($"First contestant with more than 10 invalid votes: {firstInvalidOver10Name}");
}
else
{
    Console.WriteLine("No contestant had more than 10 invalid votes.");
}

//f.
Console.WriteLine("Contestants with 50 or more valid votes:");
for (int i = 0; i < contestants.Count; i++)
{
    if (contestants[i].ValidVotes >= 50)
    {
        Console.WriteLine($"{contestants[i].Name}: {contestants[i].ValidVotes} valid votes");
    }
}
#endregion

public record Competition(string Class, int Point);
public record WebPage(string Monogram, int LikeCount);
public record Contestant(string Name, int InvalidVotes, int ValidVotes);
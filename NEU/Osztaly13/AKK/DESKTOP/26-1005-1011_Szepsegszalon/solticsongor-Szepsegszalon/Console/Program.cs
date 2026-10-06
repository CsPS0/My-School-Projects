using szalonLib;

DataStore.InitCSV();

var data = DataStore.Instance!;

string nev = data.Alkalmazottak_.Select(x => x.Nev == "").ToList();
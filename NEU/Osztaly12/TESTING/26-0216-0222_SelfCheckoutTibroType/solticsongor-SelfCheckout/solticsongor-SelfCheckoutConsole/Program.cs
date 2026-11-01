using selfCheckoutLib;

SelfCheckout checkout = new SelfCheckout();

bool running = true;
while (running)
{
    Console.Clear();
    Console.WriteLine("--- Önkiszolgáló Kassza ---");
    if (!checkout.IsActive)
    {
        Console.WriteLine("1. Új bevásárlás megkezdése");
        Console.WriteLine("0. Kilépés");
    }
    else
    {
        Console.WriteLine("2. Termék beolvasása");
        Console.WriteLine("3. Termék törlése (sztornózás)");
        Console.WriteLine("4. Tárolt tételek kilistázása");
        Console.WriteLine("5. Végösszeg kiszámítása");
        Console.WriteLine("6. Fizetés (visszajáró kiszámítása)");
        Console.WriteLine("0. Kilépés (Vigyázat: az aktív bevásárlás elveszik)");
    }

    Console.Write("\nVálasszon egy opciót: ");
    string? choice = Console.ReadLine();

    if (choice == null) continue;

    switch (choice)
    {
        case "1":
            StartPurchase();
            break;
        case "2":
            ScanProduct();
            break;
        case "3":
            DeleteProduct();
            break;
        case "4":
            ListItems();
            break;
        case "5":
            ShowTotal();
            break;
        case "6":
            Pay();
            break;
        case "0":
            running = false;
            break;
        default:
            Console.WriteLine("Érvénytelen választás. Nyomjon meg egy gombot a folytatáshoz...");
            Console.ReadKey();
            break;
    }
}

void StartPurchase()
{
    try
    {
        checkout.StartNewPurchase();
        Console.WriteLine("Bevásárlás megkezdve. Nyomjon meg egy gombot...");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hiba: {ex.Message}");
    }
    Console.ReadKey();
}

void ScanProduct()
{
    if (!checkout.IsActive) { Console.WriteLine("Nincs aktív bevásárlás!"); Console.ReadKey(); return; }

    Console.Write("Termék neve: ");
    string? name = Console.ReadLine();
    if (string.IsNullOrEmpty(name))
    {
        Console.WriteLine("Érvénytelen név!");
        Console.ReadKey();
        return;
    }

    Console.Write("Termék ára: ");
    if (double.TryParse(Console.ReadLine(), out double price))
    {
        try
        {
            checkout.ScanProduct(name, price);
            Console.WriteLine("Termék hozzáadva.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Érvénytelen ár!");
    }
    Console.ReadKey();
}

void DeleteProduct()
{
    if (!checkout.IsActive) { Console.WriteLine("Nincs aktív bevásárlás!"); Console.ReadKey(); return; }

    Console.Write("Törlendő termék neve: ");
    string? name = Console.ReadLine();
    if (string.IsNullOrEmpty(name))
    {
        Console.WriteLine("Érvénytelen név!");
        Console.ReadKey();
        return;
    }

    if (checkout.DeleteProduct(name))
    {
        Console.WriteLine("Termék törölve.");
    }
    else
    {
        Console.WriteLine("Nem található ilyen termék.");
    }
    Console.ReadKey();
}

void ListItems()
{
    if (!checkout.IsActive) { Console.WriteLine("Nincs aktív bevásárlás!"); Console.ReadKey(); return; }

    var items = checkout.GetItems();
    if (items.Count == 0)
    {
        Console.WriteLine("A kosár üres.");
    }
    else
    {
        Console.WriteLine("Kosár tartalma:");
        foreach (var item in items)
        {
            Console.WriteLine($"- {item.Name}: {item.Price} Ft");
        }
    }
    Console.ReadKey();
}

void ShowTotal()
{
    if (!checkout.IsActive) { Console.WriteLine("Nincs aktív bevásárlás!"); Console.ReadKey(); return; }

    Console.WriteLine($"Végösszeg: {checkout.GetTotal()} Ft");
    Console.ReadKey();
}

void Pay()
{
    if (!checkout.IsActive) { Console.WriteLine("Nincs aktív bevásárlás!"); Console.ReadKey(); return; }

    double total = checkout.GetTotal();
    Console.WriteLine($"Fizetendő: {total} Ft");
    Console.Write("Befizetett összeg: ");
    if (double.TryParse(Console.ReadLine(), out double amountPaid))
    {
        try
        {
            double change = checkout.Pay(amountPaid);
            Console.WriteLine($"Fizetés sikeres! Visszajáró: {change} Ft");
            Console.WriteLine("Köszönjük a vásárlást!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Érvénytelen összeg!");
    }
    Console.ReadKey();
}

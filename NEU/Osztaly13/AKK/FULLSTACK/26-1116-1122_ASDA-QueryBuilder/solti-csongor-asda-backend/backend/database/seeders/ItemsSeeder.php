<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class ItemsSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        $items = [
            ['event' => 'Tickled Pink', 'name' => 'Diet Coke Cola 30 x 330ml Cans', 'qty' => (int)330, 'unit' => 'ml', 'price' => (float)14.24, 'discount_price' => (float)10.00, 'image' => '5000112572933_T1.webp'],
            ['event' => 'Tickled Pink', 'name' => 'Nescafe Nescafe Original Instant Coffee 300g', 'qty' => (int)300, 'unit' => 'g', 'price' => (float)7.25, 'discount_price' => (float)6.00, 'image' => '5011546415499_T1.webp'],
            ['event' => 'Tickled Pink', 'name' => 'Taylors of Harrogate 160 Tea Bags 500g', 'qty' => (int)160, 'unit' => 'pk', 'price' => (float)6.17, 'discount_price' => (float)4.50, 'image' => '5010357112085_T1.webp'],
            ['event' => 'Tickled Pink', 'name' => 'Regina Blitz Household Towel', 'qty' => (int)1, 'unit' => 'roll', 'price' => (float)1.82, 'discount_price' => (float)2.00, 'image' => '8004260452816_T1.webp'],
            ['event' => 'Tickled Pink', 'name' => 'Napolina Rigatoni 500g', 'qty' => (int)500, 'unit' => 'g', 'price' => (float)1.00, 'discount_price' => null, 'image' => '5000232024725_T1.webp'],
            ['event' => 'Halloween', 'name' => 'Halloween Family Pumpkin Carving Kit', 'qty' => (int)9, 'unit' => 'pcs', 'price' => (float)3.00, 'discount_price' => null, 'image' => 'T_5063717307522_T1.webp'],
            ['event' => 'Halloween', 'name' => 'Halloween Blood Spray', 'qty' => (int)56, 'unit' => 'ml', 'price' => (float)2.00, 'discount_price' => null, 'image' => 'T_5063717289606_T1.webp'],
            ['event' => 'Halloween', 'name' => 'Pacific Wax Co Pumpkin Picking Candle', 'qty' => (int)510, 'unit' => 'g', 'price' => (float)3.50, 'discount_price' => null, 'image' => 'T_8904338641060_T1.webp'],
            ['event' => 'Christmas', 'name' => 'Exceptional by ASDA 4 Toffee Apple Spiced Crumble Tarts', 'qty' => (int)4, 'unit' => 'pk', 'price' => (float)2.95, 'discount_price' => null, 'image' => '5063089609859_T1.webp'],
            ['event' => 'Christmas', 'name' => 'Exceptional by ASDA Exceptional by 6 Luxury Mince Pies', 'qty' => (int)6, 'unit' => 'pk', 'price' => (float)2.84, 'discount_price' => null, 'image' => '5063089609866_T1.webp'],
            ['event' => 'Christmas', 'name' => 'Exceptional by ASDA 4 Brown Butter Flavour & Rum Mince Pies', 'qty' => (int)4, 'unit' => 'pk', 'price' => (float)2.95, 'discount_price' => null, 'image' => '5063089609828_T1.webp'],
            ['event' => 'Christmas', 'name' => 'Exceptional by ASDA 9 Luxury Mini Mince Pies', 'qty' => (int)9, 'unit' => 'pk', 'price' => (float)3.25, 'discount_price' => null, 'image' => '5063717011351_T1.webp'],
            ['event' => 'Christmas', 'name' => 'Celebrations Milk Chocolate Tub 500g', 'qty' => (int)500, 'unit' => 'g', 'price' => (float)5.00, 'discount_price' => null, 'image' => '5000159570701_T1.webp'],
            ['event' => 'Christmas', 'name' => 'ASDA Before Nine Thins 180g', 'qty' => (int)180, 'unit' => 'g', 'price' => (float)2.14, 'discount_price' => (float)2.00, 'image' => '5057172332366_T1.webp'],
            ['event' => 'Christmas', 'name' => "Paterson's Classic Shortbread Fingers 300g", 'qty' => (int)300, 'unit' => 'g', 'price' => (float)1.50, 'discount_price' => (float)1.25, 'image' => '0072417196379_T1.webp'],
            ['event' => 'Halloween', 'name' => 'ASDA Horror Emporium Scarily Spicy Devil Horns 150g', 'qty' => (int)150, 'unit' => 'g', 'price' => (float)1.00, 'discount_price' => (float)0.90, 'image' => '5063717271038_T1.webp'],
            ['event' => 'Halloween', 'name' => 'Squishmallows Original Biscuit Kit 186g', 'qty' => (int)186, 'unit' => 'g', 'price' => (float)2.50, 'discount_price' => null, 'image' => '5060202714377_T1.webp'],
            ['event' => 'Halloween', 'name' => 'Cake Décor Limited Edition Eyeball Cupcake Kit 241g', 'qty' => (int)241, 'unit' => 'g', 'price' => (float)2.50, 'discount_price' => null, 'image' => '5060234980511_T1.webp'],
            ['event' => 'Halloween', 'name' => 'Haribo Starmix Minis Fruit & Cola Flavour Jelly & Foam Sweets 22 x 16g Fun Size Bags Multipack 352g', 'qty' => (int)352, 'unit' => 'g', 'price' => (float)2.98, 'discount_price' => (float)2.00, 'image' => '5012035951214_T1.webp'],
            ['event' => 'Halloween', 'name' => 'Haribo Trick or Treat 40 Spooky Mi̇ni Bags', 'qty' => (int)640, 'unit' => 'g', 'price' => (float)3.00, 'discount_price' => null, 'image' => '5012035998059_T1.webp'],
            ['event' => 'Halloween', 'name' => 'Haribo HARIBO Bone Shakers 160g', 'qty' => (int)160, 'unit' => 'g', 'price' => (float)1.00, 'discount_price' => null, 'image' => '5012035973124_T1.webp'],
            ['event' => 'Tickled Pink', 'name' => 'Warburtons Limited Edition Mega Thick Toastie Soft & Sliced White Loaf 800g', 'qty' => (int)800, 'unit' => 'g', 'price' => (float)1.40, 'discount_price' => null, 'image' => '5010044011585_T1.webp'],
            ['event' => 'Tickled Pink', 'name' => 'Belvoir Farm Sparkling Elderflower 750ml', 'qty' => (int)750, 'unit' => 'ml', 'price' => (float)3.47, 'discount_price' => (float)2.50, 'image' => '5022019010110_T1.webp'],
        ];

        DB::table('items')->insert(
            $items
        );
    }
}

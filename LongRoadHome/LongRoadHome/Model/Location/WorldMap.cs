using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Resources;
using uk.ac.dundee.arpond.longRoadHome.Properties;
using uk.ac.dundee.arpond.longRoadHome.Model.Location;
using System.Windows.Controls;

namespace uk.ac.dundee.arpond.longRoadHome.Model.Location
{
    /// <summary>
    /// Adapted from http://opengameart.org/content/natural-word
    /// </summary>
    public class WorldMap
    {
        public SortedList<int, System.Windows.Point> buttonAreas { get; set; }
        public List<Tile> tileList { get; set; }
        public Bitmap tmpBitmap { get; set; }
        private const int HEIGHT = 125;
        private const int BIOME_SIZE = 124;
        private const int WIDTH = 109;
        private const int FORTY = 40;
        private const int EIGHTY = 80;
        private const int TILE_WIDTH = 19;
        private const int TILE_HEIGHT = 23;

        public WorldMap(IList<DummyLocation> locations)
        {
            tileList = new List<Tile>();
            buttonAreas = new SortedList<int, System.Windows.Point>();
            CreateMap();
            NewBiome(41, 400); // Tree
            NewBiome(1, 200); // Water
            NewBiome(141, 120); // Grass
            NewBiome(61, 50); // Snow
            GetDetail(201, 21, 80);
            GetDetail(161, 21, 200);
            GetDetail(121, 21, 400);
            GetDetail(181, 61, 450);
            GetDetail(221, 21, 50);
            GetDetail(241, 21, 150);
            GetDetail(261, 21, 150);
            PopulateLocations(locations);
            Generate();
        }

        public void CreateMap()
        {
            if (tmpBitmap != null)
            {
                tmpBitmap.Dispose();
            }
            tmpBitmap = new Bitmap(longRoadHome.Properties.Resources.t);
            tmpBitmap = tmpBitmap.Clone(new Rectangle(21, 0, 19, 23), tmpBitmap.PixelFormat);

            int b = 5;
            for (int a = 0; a <= HEIGHT; a++)
            {
                if (b == 0)
                {
                    b = 9;
                }
                else
                {
                    b = 0;
                }
                for (int i = 0; i <= WIDTH; i++)
                {
                    Tile t = new Tile()
                    {
                        Type = 21,
                        Image = new Bitmap(tmpBitmap),
                        Position = new Point((i * 18) + 10 + b, (a * 22) - (8 * a) + 5),
                        HasLocation = false,
                        LocationID = 0
                    };
                    tileList.Add(t);
                }
            }
            tmpBitmap.Dispose();
        }

        public void NewBiome(int tile, int loop)
        {
            tmpBitmap = new Bitmap(longRoadHome.Properties.Resources.t);
            tmpBitmap = tmpBitmap.Clone(new Rectangle(tile, 0, 19, 23), tmpBitmap.PixelFormat);

            Random rnd = new Random();
            for (int a = 0; a <= loop; a++)
            {
                int z = rnd.Next(0, (HEIGHT * WIDTH) + BIOME_SIZE);
                for (int i = 0; i <= 4; i++)
                {
                    if (z + i > 0 && z + i < ((HEIGHT * WIDTH) + BIOME_SIZE))
                    {
                        tileList[z + i].Image = tmpBitmap;
                    }
                    if (z + i - FORTY > 0 && z + i - FORTY < ((HEIGHT * WIDTH) + BIOME_SIZE) && i < 4)
                    {
                        tileList[z + i - FORTY].Image = tmpBitmap;
                        tileList[z + i - FORTY].Type = tile;
                    }
                    if ((z + i + FORTY) > 0 && (z + i + FORTY < ((HEIGHT * WIDTH) + BIOME_SIZE)) && i < 4)
                    {
                        tileList[z + i + FORTY].Image = tmpBitmap;
                        tileList[z + i + FORTY].Type = tile;
                    }
                    if ((z + i - EIGHTY) > 0 && (z + i - EIGHTY < ((HEIGHT * WIDTH) + BIOME_SIZE)) && i == 2)
                    {
                        tileList[z + i - EIGHTY].Image = tmpBitmap;
                        tileList[z + i - EIGHTY].Type = tile;
                    }
                    if ((z + i + EIGHTY) > 0 && (z + i + EIGHTY < ((HEIGHT * WIDTH) + BIOME_SIZE)) && i == 2)
                    {
                        tileList[z + i + EIGHTY].Image = tmpBitmap;
                        tileList[z + i + EIGHTY].Type = tile;
                    }
                }
            }
        }

        public void GetDetail(int newTile, int previousTile, int loop)
        {
            Random rnd = new Random();
            tmpBitmap = new Bitmap(longRoadHome.Properties.Resources.t);
            tmpBitmap = tmpBitmap.Clone(new Rectangle(newTile, 0, 19, 23), tmpBitmap.PixelFormat);

            for (int a = 0; a <= loop; a++)
            {
                int z = rnd.Next(0, (HEIGHT * WIDTH) + BIOME_SIZE);
                if (tileList[z].Type == previousTile)
                {
                    tileList[z].Image = tmpBitmap;
                    tileList[z].Type = newTile;
                }
            }
        }

        public void PopulateLocations(IList<DummyLocation> locations)
        {
            Random rnd = new Random();
            foreach (DummyLocation location in locations)
            {
                int tileNumber;
                do
                {
                    tileNumber = rnd.Next(tileList.Count);
                } while (tileNumber % WIDTH == 0 || tileNumber < WIDTH || tileNumber > WIDTH * HEIGHT - WIDTH || checkForLocations(tileNumber));

                tileList[tileNumber].HasLocation = true;
                tileList[tileNumber].LocationID = location.GetLocationID();
            }
        }

        private bool checkForLocations(int i)
        {
            bool surroundingHasLocation = tileList[i].HasLocation;

            List<int> toCheck = new List<int>() { i - WIDTH - 1, i - WIDTH, i - WIDTH + 1, i - 1, i + 1, i + WIDTH - 1, i + WIDTH, i + WIDTH + 1 };

            foreach (int check in toCheck)
            {
                if (check > 0 && check < tileList.Count)
                {
                    surroundingHasLocation |= tileList[check].HasLocation;
                    if (surroundingHasLocation)
                    {
                        break;
                    }
                }
            }
            return surroundingHasLocation;
        }

        public void Generate()
        {
            tmpBitmap = new Bitmap(2000, 1800);

            Graphics graph = Graphics.FromImage(tmpBitmap);
            for (int i = 0; i <= (HEIGHT * WIDTH) + BIOME_SIZE; i++)
            {
                graph.DrawImage(tileList[i].Image, tileList[i].Position);
                if (tileList[i].HasLocation)
                {
                    Point position = tileList[i].Position;

                    //var temp = new Tuple<System.Windows.Point, int>(new System.Windows.Point(position.X, position.Y), tileList[i].LocationID);
                    buttonAreas.Add(tileList[i].LocationID, new System.Windows.Point(position.X, position.Y));
                }
            }
            graph.Dispose();

        }
    }

    public class Tile
    {
        public Bitmap Image { get; set; }
        public Point Position { get; set; }
        public int Type { get; set; }
        public bool HasLocation { get; set; }
        public int LocationID { get; set; }
    }
}

using Extensions;

public class Day5 : ISolution
{
    public string Name => "Day 5";

    List<(LongRange current, LongRange target)> seedToSoilMap = new();
    List<(LongRange current, LongRange target)> soilToFertilizerMap = new();
    List<(LongRange current, LongRange target)> fertilizerToWaterMap = new();
    List<(LongRange current, LongRange target)> waterToLightMap = new();
    List<(LongRange current, LongRange target)> lightToTemperatureMap = new();
    List<(LongRange current, LongRange target)> temperatureToHumidityMap = new();
    List<(LongRange current, LongRange target)> humidityToLocationMap = new();
    public void Part1()
    {
        string input = File.ReadAllText(Path.Combine(Program.DataPath, "5/test.txt"));
        string firstLine = File.ReadLines(Path.Combine(Program.DataPath, "5/test.txt")).First();
        List<long> seeds = new();
        Parse(input,firstLine,ref seeds,ref seedToSoilMap,ref soilToFertilizerMap,ref fertilizerToWaterMap,ref waterToLightMap,ref lightToTemperatureMap,ref temperatureToHumidityMap,ref humidityToLocationMap);
        List<(long seed, long location)> locations = new();
        foreach(long seed in seeds)
        {
            locations.Add(GetLocation(seed));
        }
    }
    public (long seed, long location) GetLocation(long seed)
    {
        var stsr = seedToSoilMap.Where(x => x.current.IsWithin(seed));
        long sts = 0;
        if(stsr.Count() == 0) sts = seed;
        else sts = (seed - stsr.First().current.Start) + stsr.First().target.Start;
        Console.WriteLine($"Seed {seed} -> Soil:{sts} ");

        var stfr = soilToFertilizerMap.Where(x => x.current.IsWithin(sts));
        long stf = 0;
        if(stfr.Count() == 0) stf = sts;
        else stf = (sts - stfr.First().current.Start) + stfr.First().target.Start;
        Console.WriteLine($"Soil {sts} -> Fertilizer:{stf} ");

        var ftwr = fertilizerToWaterMap.Where(x => x.current.IsWithin(stf));
        long ftw = 0;
        if(ftwr.Count() == 0) ftw = stf;
        else ftw = (stf - ftwr.First().current.Start) + ftwr.First().target.Start;
        Console.WriteLine($"Fertilizer {stf} -> Water:{ftw} ");

        var wtlr = waterToLightMap.Where(x => x.current.IsWithin(ftw));
        long wtl = 0;
        if(wtlr.Count() == 0) wtl = ftw;
        else wtl = (ftw - wtlr.First().current.Start) + wtlr.First().target.Start;
        Console.WriteLine($"Water {ftw} -> Light:{wtl} ");

        var lttr = lightToTemperatureMap.Where(x => x.current.IsWithin(wtl));
        long ltt = 0;
        if(lttr.Count() == 0) ltt = wtl;
        else ltt = (wtl - lttr.First().current.Start) + lttr.First().target.Start;
        Console.WriteLine($"Light {wtl} -> Temperature:{ltt} ");

        var tthr = temperatureToHumidityMap.Where(x => x.current.IsWithin(ltt));
        long tth = 0;
        if(tthr.Count() == 0) tth = ltt;
        else tth = (ltt - tthr.First().current.Start) + tthr.First().target.Start;
        Console.WriteLine($"Temperature {ltt} -> Humidity:{tth} ");

        var htlr = humidityToLocationMap.Where(x => x.current.IsWithin(tth));
        long htl = 0;
        if(htlr.Count() == 0) htl = tth;
        else htl = (tth - htlr.First().current.Start) + htlr.First().target.Start;
        Console.WriteLine($"Humidity {tth} -> Location:{htl} ");

        Console.WriteLine();
        return (seed,htl);
    }

    public void Part2()
    {
        throw new NotImplementedException();
    }
    // ihateparsingihateparsingihateparsingihateparsingihate
    public void Parse(string input, string firstLine, ref List<long> seeds,ref List<(LongRange current, LongRange target)> seedToSoilMap,ref List<(LongRange current, LongRange target)> soilToFertilizerMap,ref List<(LongRange current, LongRange target)> fertilizerToWaterMap,ref List<(LongRange current, LongRange target)> waterToLightMap,ref List<(LongRange current, LongRange target)> lightToTemperatureMap,ref List<(LongRange current, LongRange target)> temperatureToHumidityMap,ref List<(LongRange current, LongRange target)> humidityToLocationMap)
    {
        foreach(var seed in firstLine.Replace("seeds: ","").Split(" "))seeds.Add(long.Parse(seed));
        int seedToSoilIndex = input.IndexOf("seed-to-soil map:")+17;
        int seedToSoilEndIndex = input.IndexOf("soil-to-fertilizer map:");
        string seedToSoil = input[seedToSoilIndex..seedToSoilEndIndex].Trim();
        foreach(string line in seedToSoil.Split("\n"))
        {
            string[] numbers = line.Split(" ");
            long from = long.Parse(numbers[0]);
            long to = long.Parse(numbers[1]);
            long length = long.Parse(numbers[2]);
            seedToSoilMap.Add((new LongRange(from,from+length),new LongRange(to,to+length)));
        }

        int soilToFertilizerIndex = input.IndexOf("soil-to-fertilizer map:")+23;
        int soilToFertilizerEndIndex = input.IndexOf("fertilizer-to-water map:");
        string soilToFertilizer = input[soilToFertilizerIndex..soilToFertilizerEndIndex].Trim();
        foreach(string line in soilToFertilizer.Split("\n"))
        {
            string[] numbers = line.Split(" ");
            long from = long.Parse(numbers[0]);
            long to = long.Parse(numbers[1]);
            long length = long.Parse(numbers[2]);
            soilToFertilizerMap.Add((new LongRange(from,from+length),new LongRange(to,to+length)));
        }

        int fertilizerToWaterIndex = input.IndexOf("fertilizer-to-water map:")+24;
        int fertilizerToWaterEndIndex = input.IndexOf("water-to-light map:");
        string fertilizerToWater = input[fertilizerToWaterIndex..fertilizerToWaterEndIndex].Trim();
        foreach(string line in fertilizerToWater.Split("\n"))
        {
            string[] numbers = line.Split(" ");
            long from = long.Parse(numbers[0]);
            long to = long.Parse(numbers[1]);
            long length = long.Parse(numbers[2]);
            fertilizerToWaterMap.Add((new LongRange(from,from+length),new LongRange(to,to+length)));
        }

        int waterToLightIndex = input.IndexOf("water-to-light map:")+19;
        int waterToLightEndIndex = input.IndexOf("light-to-temperature map:");
        string waterToLight = input[waterToLightIndex..waterToLightEndIndex].Trim();
        foreach(string line in waterToLight.Split("\n"))
        {
            string[] numbers = line.Split(" ");
            long from = long.Parse(numbers[0]);
            long to = long.Parse(numbers[1]);
            long length = long.Parse(numbers[2]);
            waterToLightMap.Add((new LongRange(from,from+length),new LongRange(to,to+length)));
        }

        int lightToTemperatureIndex = input.IndexOf("light-to-temperature map:")+25;
        int lightToTemperatureEndIndex = input.IndexOf("temperature-to-humidity map:");
        string lightToTemperature = input[lightToTemperatureIndex..lightToTemperatureEndIndex].Trim();
        foreach(string line in lightToTemperature.Split("\n"))
        {
            string[] numbers = line.Split(" ");
            long from = long.Parse(numbers[0]);
            long to = long.Parse(numbers[1]);
            long length = long.Parse(numbers[2]);
            lightToTemperatureMap.Add((new LongRange(from,from+length),new LongRange(to,to+length)));
        }

        int temperatureToHumidityIndex = input.IndexOf("temperature-to-humidity map:")+29;
        int temperatureToHumidityEndIndex = input.IndexOf("humidity-to-location map:");
        string temperatureToHumidity = input[temperatureToHumidityIndex..temperatureToHumidityEndIndex].Trim();
        foreach(string line in temperatureToHumidity.Split("\n"))
        {
            string[] numbers = line.Split(" ");
            long from = long.Parse(numbers[0]);
            long to = long.Parse(numbers[1]);
            long length = long.Parse(numbers[2]);
            temperatureToHumidityMap.Add((new LongRange(from,from+length),new LongRange(to,to+length)));
        }

        int humidityToLocationIndex = input.IndexOf("humidity-to-location map:")+26;
        int humidityToLocationEndIndex = input.Length;
        string humidityToLocation = input[humidityToLocationIndex..humidityToLocationEndIndex].Trim();
        foreach(string line in humidityToLocation.Split("\n"))
        {
            string[] numbers = line.Split(" ");
            long from = long.Parse(numbers[0]);
            long to = long.Parse(numbers[1]);
            long length = long.Parse(numbers[2]);
            humidityToLocationMap.Add((new LongRange(from,from+length),new LongRange(to,to+length)));
        }
    }
}
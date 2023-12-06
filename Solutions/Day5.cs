using System.Collections.Concurrent;
using Extensions;
using Utility;

public class Day5 : ISolution
{
    public string Name => "Day 5: If You Give A Seed A Fertilizer";

    List<Converter> seedToSoilMap = new();
    List<Converter> soilToFertilizerMap = new();
    List<Converter> fertilizerToWaterMap = new();
    List<Converter> waterToLightMap = new();
    List<Converter> lightToTemperatureMap = new();
    List<Converter> temperatureToHumidityMap = new();
    List<Converter> humidityToLocationMap = new();
    public void Part1()
    {
        string input = File.ReadAllText(Path.Combine(Program.DataPath, "5/input.txt"));
        string firstLine = File.ReadLines(Path.Combine(Program.DataPath, "5/input.txt")).First();
        List<long> seeds = new();
        Parse(input,firstLine,seeds,seedToSoilMap,soilToFertilizerMap,fertilizerToWaterMap,waterToLightMap,lightToTemperatureMap,temperatureToHumidityMap,humidityToLocationMap);
        List<(long seed, long location)> locations = new();
        foreach(long seed in seeds)
        {
            locations.Add(GetLocation(seed));
        }
        locations = locations.OrderBy(x => x.location).ToList();
        ConsoleEx.WriteLineColor(("The closest location is ",ConsoleColor.White), (locations[0].location, ConsoleColor.Green), (" for seed ",ConsoleColor.White), (locations[0].seed, ConsoleColor.Green), ("\n",ConsoleColor.White));
    }
    public (long location, long seed) GetSeed(long location)
    {
        var htlr = humidityToLocationMap.Where(x => x.target.IsWithin(location));
        long htl = 0;
        if(htlr.Count() == 0) htl = location;
        else htl = (location - htlr.First().target.Start) + htlr.First().current.Start;
        //Console.WriteLine($"Location {location} -> Humidity:{htl} ");

        var tthr = temperatureToHumidityMap.Where(x => x.target.IsWithin(htl));
        long tth = 0;
        if(tthr.Count() == 0) tth = htl;
        else tth = (htl - tthr.First().target.Start) + tthr.First().current.Start;
        //Console.WriteLine($"Humidity {htl} -> Temperature:{tth} ");

        var lttr = lightToTemperatureMap.Where(x => x.target.IsWithin(tth));
        long ltt = 0;
        if(lttr.Count() == 0) ltt = tth;
        else ltt = (tth - lttr.First().target.Start) + lttr.First().current.Start;
        //Console.WriteLine($"Temperature {tth} -> Light:{ltt} ");

        var wtlr = waterToLightMap.Where(x => x.target.IsWithin(ltt));
        long wtl = 0;
        if(wtlr.Count() == 0) wtl = ltt;
        else wtl = (ltt - wtlr.First().target.Start) + wtlr.First().current.Start;
        //Console.WriteLine($"Light {ltt} -> Water:{wtl} ");

        var ftwr = fertilizerToWaterMap.Where(x => x.target.IsWithin(wtl));
        long ftw = 0;
        if(ftwr.Count() == 0) ftw = wtl;
        else ftw = (wtl - ftwr.First().target.Start) + ftwr.First().current.Start;
        //Console.WriteLine($"Water {wtl} -> Fertilizer:{ftw} ");

        var stfr = soilToFertilizerMap.Where(x => x.target.IsWithin(ftw));
        long stf = 0;
        if(stfr.Count() == 0) stf = ftw;
        else stf = (ftw - stfr.First().target.Start) + stfr.First().current.Start;
        //Console.WriteLine($"Fertilizer {ftw} -> Soil:{stf} ");

        var stsr = seedToSoilMap.Where(x => x.target.IsWithin(stf));
        long sts = 0;
        if(stsr.Count() == 0) sts = stf;
        else sts = (stf - stsr.First().target.Start) + stsr.First().current.Start;
        //Console.WriteLine($"Soil {stf} -> Seed:{sts} \n");

        return (sts,location);
    }
    public (long seed, long location) GetLocation(long seed)
    {
        var stsr = seedToSoilMap.Where(x => x.current.IsWithin(seed));
        long sts = 0;
        if(stsr.Count() == 0) sts = seed;
        else sts = (seed - stsr.First().current.Start) + stsr.First().target.Start;
        //Console.WriteLine($"Seed {seed} -> Soil:{sts} ");

        var stfr = soilToFertilizerMap.Where(x => x.current.IsWithin(sts));
        long stf = 0;
        if(stfr.Count() == 0) stf = sts;
        else stf = (sts - stfr.First().current.Start) + stfr.First().target.Start;
        //Console.WriteLine($"Soil {sts} -> Fertilizer:{stf} ");

        var ftwr = fertilizerToWaterMap.Where(x => x.current.IsWithin(stf));
        long ftw = 0;
        if(ftwr.Count() == 0) ftw = stf;
        else ftw = (stf - ftwr.First().current.Start) + ftwr.First().target.Start;
        //Console.WriteLine($"Fertilizer {stf} -> Water:{ftw} ");

        var wtlr = waterToLightMap.Where(x => x.current.IsWithin(ftw));
        long wtl = 0;
        if(wtlr.Count() == 0) wtl = ftw;
        else wtl = (ftw - wtlr.First().current.Start) + wtlr.First().target.Start;
        //Console.WriteLine($"Water {ftw} -> Light:{wtl} ");

        var lttr = lightToTemperatureMap.Where(x => x.current.IsWithin(wtl));
        long ltt = 0;
        if(lttr.Count() == 0) ltt = wtl;
        else ltt = (wtl - lttr.First().current.Start) + lttr.First().target.Start;
        //Console.WriteLine($"Light {wtl} -> Temperature:{ltt} ");

        var tthr = temperatureToHumidityMap.Where(x => x.current.IsWithin(ltt));
        long tth = 0;
        if(tthr.Count() == 0) tth = ltt;
        else tth = (ltt - tthr.First().current.Start) + tthr.First().target.Start;
        //Console.WriteLine($"Temperature {ltt} -> Humidity:{tth} ");

        var htlr = humidityToLocationMap.Where(x => x.current.IsWithin(tth));
        long htl = 0;
        if(htlr.Count() == 0) htl = tth;
        else htl = (tth - htlr.First().current.Start) + htlr.First().target.Start;
        //Console.WriteLine($"Humidity {tth} -> Location:{htl} \n");

        return (seed,htl);
    }

    // This one took 42 minutes to run on a Ryzen 5 5600G
    // It didn't even finish after 2 hours before i threaded it
    public void Part2()
    {
        string input = File.ReadAllText(Path.Combine(Program.DataPath, "5/input.txt"));
        string firstLine = File.ReadLines(Path.Combine(Program.DataPath, "5/input.txt")).First();
        List<long> seeds = new();
        Parse(input,firstLine,seeds,seedToSoilMap,soilToFertilizerMap,fertilizerToWaterMap,waterToLightMap,lightToTemperatureMap,temperatureToHumidityMap,humidityToLocationMap);
        List<long> seeds1 = new();
        List<long> seeds2 = new();
        for (int i = 0; i < seeds.Count; i++)
        {
            if(i % 2 == 0) seeds1.Add(seeds[i]);
            else seeds2.Add(seeds[i]);
        }
        List<LongRange> seedRanges = new();
        foreach(var seed in Quick.Range(0,seeds1.Count-1)) seedRanges.Add(new LongRange(seeds1[seed],seeds1[seed]+seeds2[seed]));
        seedRanges = seedRanges.OrderBy(x => x.Start).ToList();
        ConcurrentBag<(long seed, long location)> results = new();
        // Just learned about this, life saver
        Parallel.ForEach(seedRanges, seedRange =>
        {
            List<(long seed, long location)> thisResults = new();
            Console.WriteLine($"Checking seeds {seedRange.Start} -> {seedRange.End}");
            (long seed, long location) location = GetLocation(seedRange.Start);
            for(long i = seedRange.Start; i < seedRange.End; i++)
            {
                var newLocation = GetLocation(i);
                // I used to just add the location then sort it and take the first one
                // Windows didn't like that, 10GB used at worse on 16 GB (15.8 with around 3-5 for the System))
                // Now thanks to this, it never goes over 12 MB
                if(location.location > newLocation.location)
                {
                    thisResults.Remove(location);
                    location = newLocation;
                    thisResults.Add((newLocation.seed,newLocation.location));
                }
            }
            ConsoleEx.WriteLineColor($"Done checking seeds {seedRange.Start} -> {seedRange.End}",ConsoleColor.Green);
            if(thisResults.OrderBy(x => x.location).Count() > 0) results.Add(thisResults.OrderBy(x => x.location).First());
        });
        var order = results.OrderBy(x => x.location);
        (long seed, long location) result = order.Count() > 0 ? order.First() : (0,0);
        ConsoleEx.WriteLineColor(("The closest location is ",ConsoleColor.White), (result.location, ConsoleColor.Green), (" for seed ",ConsoleColor.White), (result.seed, ConsoleColor.Green), ("\n",ConsoleColor.White));
    }

    /// <summary>
    /// ihateparsingihateparsingihateparsingihateparsingihate
    /// </summary>
    public void Parse(string input, string firstLine, List<long> seeds,List<Converter> seedToSoilMap,List<Converter> soilToFertilizerMap,List<Converter> fertilizerToWaterMap,List<Converter> waterToLightMap,List<Converter> lightToTemperatureMap,List<Converter> temperatureToHumidityMap,List<Converter> humidityToLocationMap)
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
            seedToSoilMap.Add(new Converter(to, from, length));
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
            soilToFertilizerMap.Add(new Converter(to, from, length));
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
            fertilizerToWaterMap.Add(new Converter(to, from, length));
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
            waterToLightMap.Add(new Converter(to, from, length));
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
            lightToTemperatureMap.Add(new Converter(to, from, length));
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
            temperatureToHumidityMap.Add(new Converter(to, from, length));
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
            humidityToLocationMap.Add(new Converter(to, from, length));
        }
    }
    public class Converter
    {
        public LongRange current, target;

        public Converter(long current, long target, long length)
        {
            this.current = new LongRange(current,current+length);
            this.target = new LongRange(target,target+length);
        }   
    }
}
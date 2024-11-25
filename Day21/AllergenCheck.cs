namespace AoC20.Day21
{
    class Food
    {
        public List<string> Ingredients = [];
        public List<string> Allergens   = [];

        public Food(string inputLine)
        {
            //mxmxvkd kfcds sqjhc nhms(contains dairy, fish)
            var parts = inputLine.Split("(contains ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Ingredients = parts[0].Split(" ").ToList();
            Allergens = parts[1].Replace(")", "").Replace(",", "").Split(" ").ToList();
        }
    }

    class AllergenCheck
    {
        List<Food> foods = [];
        public void ParseInput(List<string> input)
            => input.ForEach(x => foods.Add(new Food(x)));

        int SolvePart1()
        {
            var allAllergens    = foods.SelectMany(x => x.Allergens).Distinct();
            var allIngredients  = foods.SelectMany(x => x.Ingredients).Distinct();
            Dictionary<string, HashSet<string>> allergenPresence = new();

            foreach (var allergen in allAllergens)
            {
                var listOfFoods = foods.Where(x => x.Allergens.Contains(allergen)).ToList();

                foreach (var food in listOfFoods)
                    if (allergenPresence.ContainsKey(allergen))
                        allergenPresence[allergen].IntersectWith(food.Ingredients);
                    else
                        allergenPresence[allergen] = food.Ingredients.ToHashSet();
            }

            var ingMaybeAllergens = allergenPresence.Values.SelectMany(x => x).Distinct().ToList();
            var ingNoAllergens    = allIngredients.Except(ingMaybeAllergens).Distinct().ToList();

            return foods.Sum(food => food.Ingredients.Count(ingredient => ingNoAllergens.Contains(ingredient)));
        }

        public int Solve(int part = 1)
            => SolvePart1();
    }
}

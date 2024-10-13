namespace PillagedLocationData
{
	class PillagedLocationData
	{ 
		private string _villageName = null;
		private int _numberOfEnemies = 0;

		public PillagedLocationData(string villageName, int numberOfEnemies)
		{
			_villageName = villageName;
			_numberOfEnemies = numberOfEnemies;
		}
	}

	enum PillageLocationDataContainer
	{
		EmeraldVillage,
		SpringVillage,
		RottenVillage,
		LesserVillage,
		GreatVillage,
		WhiteHillsVillage,
		KingsVillage,
		YellowTownVillage
	}
}
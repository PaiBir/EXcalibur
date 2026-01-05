using UnityEngine;

public class ClimateCell
{
	public enum CellType
	{
		Atmospheric,
		LandSurface,
		Oceanic,
		Generic
	}
	public CellType cellClassification;
	protected float cellEnergy;
	protected Chemical[] cellChemicals;
}

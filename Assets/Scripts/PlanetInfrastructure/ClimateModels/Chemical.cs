using UnityEngine;

public class Chemical
{
    public enum ChemicalID
	{
		H2O, //Water
		O2, //Oxygen (gas)
		O3, //Ozone
		CO2, //Carbon Dioxide
		SO3, //Sulfur Trioxide
		SO4, //Sulfur Tetroxide
		H2SO4, //Sulfuric Acid
		H2, //Hydrogen (gas)
		Fe, //Iron
		Au, //Gold

	}
	public enum Unit
	{
		ppt, //Parts Per Thousand (Concentration)
		ppm, //Parts Per Million (Concentration)
		ppb, //Parts Per Billion (Concentration)
		atm //Atmosperes (Pressure)
	}
	public ChemicalID ChemicalMakeup;
	public double ChemicalConcentration;

}

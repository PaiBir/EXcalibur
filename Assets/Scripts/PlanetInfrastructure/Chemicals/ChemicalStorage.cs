using System;

//The Chemical Vault is built off of the July 2022 thermo.tdat dataset supplied freely by the Geochemist's Workbench
//Future versions of this should not be static, so that an optimized dataset can be fed through
public class ChemicalVault
{
	public enum ThermoProperties
	{
		temperature,
		pressure,
		debyeHuckleA,
		debyeHuckleB,
		bdot,
		co2_1,
		co2_2,
		co2_3,
		co2_4,
		h2o_1,
		h2o_2,
		h2o_3,
		h2o_4
	}
	[Serializable]
	public struct ThermoKey
	{
		public ThermoProperties Property;
		public double[] val;

		public ThermoKey(ThermoProperties _p, double[] _v)
		{
			Property = _p;
			val = _v;
		}
	}
	public enum Element //American element names
	{
		Silver,
		Aluminum,
		Americium,
		Arsenic,
		Gold,
		Boron,
		Barium,
		Bromine,
		Carbon,
		Calcium,
		Chlorine,
		Cobalt,
		Chromium,
		Cesium,
		Copper,
		Europium,
		Flourine,
		Iron,
		Hydrogen,
		Mercury,
		Iodine,
		Potassium,
		Lithium,
		Magnesium,
		Manganese,
		Nitrogen,
		Sodium,
		Nickel,
		Neptunium,
		Oxygen,
		Phosphorus,
		Lead,
		Plutonium,
		Radium,
		Rubidium,
		Ruthenium,
		Sulfur,
		Selenium,
		Silicon,
		Tin,
		Strontium,
		Technetium,
		Thorium,
		Uranium,
		Vanadium,
		Zinc
	}
	[Serializable]
	public struct ElementVault
	{
		public Element element;
		public double molarweight; //In Grams

		public ElementVault(Element _e, double _m)
		{
			element = _e;
			molarweight = _m;
		}
	}

	[Serializable]
	public struct ChemicalBlock
	{
		public Element element;
		public int quantity;

		public ChemicalBlock(Element _e, int _q)
		{
			element = _e;
			quantity = _q;
		}
	}

	[Serializable]
	public struct SpeciesReference
	{
		public string name;
		public double qt;

		public SpeciesReference(string _name,  double _qt)
		{
			name = _name;
			qt = _qt;
		}
	}

	[Serializable]
	public struct Species
	{
		public string name;
		public string plainTextFormula;
		public ChemicalBlock[] formula;
		public int charge;
		public double ionSize;
		public double molarWeight;
		public SpeciesReference[] speciesInReaction;
		public double[] thermoResults;

		//Basis Species
		public Species(string _name, int _charge, double _ionSize, double _molarWeight, ChemicalBlock[] _formula)
		{
			name = _name;
			formula = _formula;
			charge = _charge;
			ionSize = _ionSize; //Angstroms
			molarWeight = _molarWeight; //Grams

			//Additional Property Handler
			plainTextFormula = null;
			speciesInReaction = null;
			thermoResults = null;
		}

		//Redox Species
		public Species(string _name, string _pTFormula, int _charge, double _ionSize, double _molarWeight, SpeciesReference[] _formula, double[] _thermo)
		{
			name = _name;
			charge = _charge;
			ionSize = _ionSize; //Angstroms
			molarWeight = _molarWeight; //Grams
			plainTextFormula = _pTFormula;
			speciesInReaction = _formula;
			thermoResults = _thermo;
			formula = null;
		}
	}

	public static ThermoKey[] ThermoKeys =
	{
		new ThermoKey(ThermoProperties.temperature,		new double[] {0.0000,		25.0000,	60.0000,	100.0000,	150.0000,	200.0000,	250.0000,	300.0000}),		//C
		new ThermoKey(ThermoProperties.pressure,		new double[] {1.0134,		1.0134,		1.0134,		1.0134,		4.7600,		15.5490,	39.7760,	85.9270}),		//Bar
		new ThermoKey(ThermoProperties.debyeHuckleA,	new double[] {0.4913,		0.5092,		0.5450,		0.5998,		0.6898,		0.8099,		0.9785,		1.2555}),		//ADH
		new ThermoKey(ThermoProperties.debyeHuckleB,	new double[] {0.3247,		0.3283,		0.3343,		0.3422,		0.3533,		0.3655,		0.3792,		0.3965}),		//ADH
		new ThermoKey(ThermoProperties.bdot,			new double[] {0.0174,		0.0410,		0.0440,		0.460,		0.0470,		0.0470,		0.0340,		0.0000}),
		new ThermoKey(ThermoProperties.co2_1,			new double[] { 0.1224,		0.1127,		0.09341,	0.08018,	0.08427,	0.09892,	0.1371,		0.1967}),
		new ThermoKey(ThermoProperties.co2_2,			new double[] {-0.004679,	-0.01049,	-0.0036,	-0.001503,	-0.01184,	-0.0104,	-0.007086,	-0.01809}),
		new ThermoKey(ThermoProperties.co2_3,			new double[] {-0.0004114,	001545,		0.00009609,	0.0005009,	0.003118,	0.001386,	-0.002887,	-0.002497}),
		new ThermoKey(ThermoProperties.co2_4,			new double[] {0.0000,		0.0000,		0.0000,		0.0000,		0.0000,		0.0000,		0.0000,		0.0000}),
		new ThermoKey(ThermoProperties.h2o_1,			new double[] {500.0000,		1.45397,	500.000,	1.5551,		1.6225,		500.0000,	500.0000,	500.0000}),
		new ThermoKey(ThermoProperties.h2o_2,			new double[] {500.0000,		0.022357,	500.0000,	0.036478,	0.045891,	500.0000,	500.0000,	500.0000}),
		new ThermoKey(ThermoProperties.h2o_3,			new double[] {500.0000,		0.0093804,	500.0000,	0.0064366,	0.0045221,	500.0000,	500.0000,	500.0000}),
		new ThermoKey(ThermoProperties.h2o_4,			new double[] {500.0000,		-0.0005362,	500.0000,	-0.0007132,	-0.0008312,	500.0000,	500.0000,	500.0000})
	};

	public static ElementVault[] elementVault =
	{
		new ElementVault(Element.Silver, 107.8680),
		new ElementVault(Element.Aluminum, 26.9815),
		new ElementVault(Element.Americium, 241.0600),
		new ElementVault(Element.Arsenic, 74.9216),
		new ElementVault(Element.Gold, 196.9665),
		new ElementVault(Element.Boron, 10.8110),
		new ElementVault(Element.Barium, 137.3300),
		new ElementVault(Element.Bromine, 79.9040),
		new ElementVault(Element.Carbon, 12.0110),
		new ElementVault(Element.Calcium, 40.0800),
		new ElementVault(Element.Chlorine, 35.4530),
		new ElementVault(Element.Cobalt, 58.9332),
		new ElementVault(Element.Chromium, 51.9960),
		new ElementVault(Element.Cesium, 132.9054),
		new ElementVault(Element.Copper, 63.5460),
		new ElementVault(Element.Europium, 151.9600),
		new ElementVault(Element.Flourine, 18.9984),
		new ElementVault(Element.Iron, 55.8470),
		new ElementVault(Element.Hydrogen, 1.0079),
		new ElementVault(Element.Mercury, 200.5900),
		new ElementVault(Element.Iodine, 126.9045),
		new ElementVault(Element.Potassium, 39.0983),
		new ElementVault(Element.Lithium, 6.9410),
		new ElementVault(Element.Magnesium, 24.3050),
		new ElementVault(Element.Manganese, 54.9380),
		new ElementVault(Element.Nitrogen, 14.0067),
		new ElementVault(Element.Sodium, 22.9898),
		new ElementVault(Element.Nickel, 58.7100),
		new ElementVault(Element.Neptunium, 237.0482),
		new ElementVault(Element.Oxygen, 15.9994),
		new ElementVault(Element.Phosphorus, 30.9738),
		new ElementVault(Element.Lead, 207.2000),
		new ElementVault(Element.Plutonium, 244.0000),
		new ElementVault(Element.Radium, 226.0250),
		new ElementVault(Element.Rubidium, 85.4670),
		new ElementVault(Element.Ruthenium, 101.07000),
		new ElementVault(Element.Sulfur, 32.0600),
		new ElementVault(Element.Selenium, 78.9600),
		new ElementVault(Element.Silicon, 28.0850),
		new ElementVault(Element.Tin, 118.6900),
		new ElementVault(Element.Strontium, 87.6200),
		new ElementVault(Element.Technetium, 97.0000),
		new ElementVault(Element.Thorium, 232.0381),
		new ElementVault(Element.Uranium, 238.0290),
		new ElementVault(Element.Vanadium, 50.9414),
		new ElementVault(Element.Zinc, 65.3800)
	};

	public static Species[] BasisSpeciesVault =
	{
		new Species("H2O",		0,	0.0,	18.0152,	new ChemicalBlock[]{new ChemicalBlock(Element.Hydrogen, 2),	new ChemicalBlock(Element.Oxygen, 1)}),
		new Species("Ag+",		1,	2.5,	107.8680,	new ChemicalBlock[]{new ChemicalBlock(Element.Silver, 1)}),
		new Species("Al+++",	3,	9.0,	26.9815,	new ChemicalBlock[]{new ChemicalBlock(Element.Aluminum, 1)}),
		new Species("Am+++",	3,	9.0,	241.0600,	new ChemicalBlock[]{new ChemicalBlock(Element.Americium, 1)}),
		new Species("As(OH)4-",	-1,	4.0,	142.9508,	new ChemicalBlock[]{new ChemicalBlock(Element.Arsenic, 1),	new ChemicalBlock(Element.Hydrogen, 4),	new ChemicalBlock(Element.Oxygen, 4)}),
		new Species("Au+",		1,	2.5,	196.9665,	new ChemicalBlock[]{new ChemicalBlock(Element.Gold, 1)}),
		new Species("B(OH)3",	0,	-0.5,	61.8329,	new ChemicalBlock[]{new ChemicalBlock(Element.Boron, 1),	new ChemicalBlock(Element.Hydrogen, 3),	new ChemicalBlock(Element.Oxygen, 3)}),
		new Species("Ba++",		2,	5.0,	137.330,	new ChemicalBlock[]{new ChemicalBlock(Element.Barium, 1)}),
		new Species("Br-",		-1,	3.0,	79.9040,	new ChemicalBlock[]{new ChemicalBlock(Element.Bromine, 1)}),
		new Species("Ca++",		2,	6.0,	40.0800,	new ChemicalBlock[]{new ChemicalBlock(Element.Calcium, 1)}),
		new Species("Cl-",		-1,	3.0,	35.4530,	new ChemicalBlock[]{new ChemicalBlock(Element.Chlorine, 1)}),
		new Species("Co++",		2,	6.0,	48.9332,	new ChemicalBlock[]{new ChemicalBlock(Element.Cobalt, 1)}),
		new Species("Cr+++",	3,	9.0,	51.9960,	new ChemicalBlock[]{new ChemicalBlock(Element.Chromium, 1)}),
		new Species("Cs+",		1,	2.5,	132.9054,	new ChemicalBlock[]{new ChemicalBlock(Element.Cesium, 1)}),
		new Species("Cu+",		1,	2.5,	63.5460,	new ChemicalBlock[]{new ChemicalBlock(Element.Copper, 1)}),
		new Species("Eu+++",	3,	2.5,	151.960,	new ChemicalBlock[]{new ChemicalBlock(Element.Europium, 1)}),
		new Species("F-",		-1,	3.5,	18.9984,	new ChemicalBlock[]{new ChemicalBlock(Element.Flourine, 1)}),
		new Species("Fe++",		2,	6.0,	55.8470,	new ChemicalBlock[]{new ChemicalBlock(Element.Iron, 1)}),
		new Species("H+",		1,	9.0,	1.0079,		new ChemicalBlock[]{new ChemicalBlock(Element.Hydrogen, 1)}),
		new Species("HCO3-",	-1,	4.5,	61.0171,	new ChemicalBlock[]{new ChemicalBlock(Element.Carbon, 1),	new ChemicalBlock(Element.Hydrogen, 1),	new ChemicalBlock(Element.Oxygen, 3)}),
		new Species("Hg++",		2,	5.0,	200.5900,	new ChemicalBlock[]{new ChemicalBlock(Element.Mercury, 1)}),
		new Species("HPO4-",	-2,	4.0,	95.9793,	new ChemicalBlock[]{new ChemicalBlock(Element.Hydrogen, 1),	new ChemicalBlock(Element.Oxygen, 4),	new ChemicalBlock(Element.Phosphorus, 1)}),
		new Species("I-",		-1,	3.0,	126.9045,	new ChemicalBlock[]{new ChemicalBlock(Element.Iodine, 1)}),
		new Species("K+",		1,	3.0,	39.0983,	new ChemicalBlock[]{new ChemicalBlock(Element.Potassium, 1)}),
		new Species("Li+",		1,	6.0,	6.9410,		new ChemicalBlock[]{new ChemicalBlock(Element.Lithium, 1)}),
		new Species("Mg++",		2,	8.0,	24.3050,	new ChemicalBlock[]{new ChemicalBlock(Element.Magnesium, 1)}),
		new Species("Mn++",		2,	6.0,	54.9830,	new ChemicalBlock[]{new ChemicalBlock(Element.Manganese, 1)}),
		new Species("Na+",		1,	4.0,	22.9898,	new ChemicalBlock[]{new ChemicalBlock(Element.Sodium, 1)}),
		new Species("Ni++",		2,	6.0,	58.7100,	new ChemicalBlock[]{new ChemicalBlock(Element.Nickel, 1)}),
		new Species("NO3-",		-1,	3.0,	62.0049,	new ChemicalBlock[]{new ChemicalBlock(Element.Nitrogen, 1),	new ChemicalBlock(Element.Oxygen, 3)}),
		new Species("Np++++",	4,	2.5,	237.0482,	new ChemicalBlock[]{new ChemicalBlock(Element.Neptunium, 1)}),
		new Species("O2(aq)",	0,	-0.5,	31.9988,	new ChemicalBlock[]{new ChemicalBlock(Element.Oxygen, 2)}),
		new Species("Pb++",		2,	4.5,	207.2000,	new ChemicalBlock[]{new ChemicalBlock(Element.Lead, 1)}),
		new Species("PuO2++",	2,	5.0,    275.9988,	new ChemicalBlock[]{new ChemicalBlock(Element.Oxygen, 2),	new ChemicalBlock(Element.Plutonium, 1)}),
		new Species("Ra++",		2,	5.0,    226.0250,	new ChemicalBlock[]{new ChemicalBlock(Element.Radium, 1)}),
		new Species("Rb+",		1,	2.5,    85.4670,	new ChemicalBlock[]{new ChemicalBlock(Element.Rubidium, 1)}),
		new Species("Ru+++",	3,	8.0,    101.0700,	new ChemicalBlock[]{new ChemicalBlock(Element.Ruthenium, 1)}),
		new Species("SeO3--",	-2,	0.0,    126.9582,	new ChemicalBlock[]{new ChemicalBlock(Element.Oxygen, 3),	new ChemicalBlock(Element.Selenium, 1)}),
		new Species("SiO2(aq)",	0,	-0.5,   60.0838,	new ChemicalBlock[]{new ChemicalBlock(Element.Oxygen, 2),	new ChemicalBlock(Element.Silicon, 1)}),
		new Species("Sn++++",	4,	11.0,   118.6900,	new ChemicalBlock[]{new ChemicalBlock(Element.Tin, 1)}),
		new Species("SO4--",	-2,	4.0,    96.0576,	new ChemicalBlock[]{new ChemicalBlock(Element.Oxygen, 4),	new ChemicalBlock(Element.Sulfur, 1)}),
		new Species("Sr++",		2,	5.0,    87.6200,	new ChemicalBlock[]{new ChemicalBlock(Element.Strontium, 1)}),
		new Species("TcO4-",	-1,	4.0,    160.9976,	new ChemicalBlock[]{new ChemicalBlock(Element.Oxygen, 4),	new ChemicalBlock(Element.Technetium, 1)}),
		new Species("Th++++",	4,	11.0,   232.0381,	new ChemicalBlock[]{new ChemicalBlock(Element.Thorium, 1)}),
		new Species("U++++",	4,	11.0,   238.0290,	new ChemicalBlock[]{new ChemicalBlock(Element.Uranium, 1)}),
		new Species("V+++",		3,	4.0,    50.9414,	new ChemicalBlock[]{new ChemicalBlock(Element.Vanadium, 1)}),
		new Species("Zn++",		2,	6.0,    65.3800,	new ChemicalBlock[]{new ChemicalBlock(Element.Zinc, 1)}),
	};

	public static Species[] RedoxCouplesVault =
	{
		new Species("O-phth)--",    "C6H4(COO)2--", -2, 4.0,    164.1172,	new SpeciesReference[] {
			new SpeciesReference("H2O", -5.0),
			new SpeciesReference("HCO3-", 8.0),
			new SpeciesReference("H+", 6.0),
			new SpeciesReference("O2(aq)", -7.5)
			}, new double[] {594.3211, 542.8292, 482.3612, 425.9738, 358.7004, 321.8658, 281.8216, 246.4849}),
		new Species("Am++++",		"Am++++",		4, 11.0,	241.0600,	new SpeciesReference[] {
			new SpeciesReference("H2O", -0.5),
			new SpeciesReference("H+", 1.0),
			new SpeciesReference("Am+++", 1.0),
			new SpeciesReference("O2(aq)", 0.25)
			}, new double[] {18.7967, 18.0815, 17.2698, 16.5278, 15.8024, 15.2312, 14.7898, 14.425}),
		new Species("AmO2+",		"AmO2+",		1,	4,		273.0588,	new SpeciesReference[] {
			new SpeciesReference("H+", -2),
			new SpeciesReference("H2O", 1),
			new SpeciesReference("Am+++", 1),
			new SpeciesReference("O2(aq)", 0.5),
			}, new double[] {16.9445, 15.2523, 13.3736, 11.6629, 9.9847, 8.6568, 7.5827, 6.6515}),
		new Species("AmO2++",		"AmO2++",		2,	5,		273.0588,	new SpeciesReference[] {
			new SpeciesReference("H+", -1),
			new SpeciesReference("H2O", .5),
			new SpeciesReference("Am+++", 1),
			new SpeciesReference("O2(aq)", .75),
			}, new double[] {22.5663, 20.6531, 18.5553, 16.6935, 14.9435, 13.6249, 12.6368, 11.8290}),
		new Species("AsH3(aq)",		"AsH3(aq)",		0,	4,		77.9453,	new SpeciesReference[] {
			new SpeciesReference("H2O", -1),
			new SpeciesReference(" As(OH)4-", 1),
			new SpeciesReference(" H+", 1),
			new SpeciesReference(" O2(aq)", -1.5),
			}, new double[] {136.4283, 124.6488, 110.775, 97.6908, 84.1285, 72.5954, 500, 500}),
		new Species("AsO4---",		"AsO4---",		-3,	4,		138.9192,	new SpeciesReference[] {
			new SpeciesReference("H2O", -1),
			new SpeciesReference(" As(OH)4-", 1),
			new SpeciesReference(" H+", -2),
			new SpeciesReference(" O2(aq)", 0.5),
			}, new double[] {-14.7706, -12.1848, -8.9694, -5.7943, -2.4459, 0.4498, 3.102, 5.514}),
		new Species("Au+++",		"Au+++",		3,	9,		196.9665,	new SpeciesReference[] {
			new SpeciesReference("H2O", -1),
			new SpeciesReference(" H+", 2),
			new SpeciesReference(" Au+", 1),
			new SpeciesReference(" O2(aq)", 0.5),
			}, new double[] {3.29, 4.3513, 5.6219, 6.8164, 8.0674, 9.1557, 10.2267, 11.4411}),
		new Species("CH3COO-",		"CH3COO-",		-1,	4.5,	59.0445,	new SpeciesReference[] {
			new SpeciesReference("HCO3-", 2),
			new SpeciesReference(" H+", 1),
			new SpeciesReference(" O2(aq)", -2),
			}, new double[] {160.6192, 146.7487, 130.6352, 115.8131, 100.9856, 89.0757, 79.0857, 70.4395}),
		new Species("CH4(aq)",		"CH4(aq)",		0,	-0.5,	16.0426,	new SpeciesReference[] {
			new SpeciesReference("H2O", 1),
			new SpeciesReference(" H+", 1),
			new SpeciesReference(" HCO3-", 1),
			new SpeciesReference(" O2(aq)", -2),
			}, new double[] {157.892, 144.108, 127.936, 112.88, 97.706, 85.288, 74.75, 65.65}),
		new Species("ClO4-",		"ClO4-",		-1,	3.5,	99.4506,	new SpeciesReference[] {
			new SpeciesReference("Cl-", 1),
			new SpeciesReference(" O2(aq)", 2),
			}, new double[] {16.7898, 15.7107, 14.6883, 13.8819, 13.293, 12.9533, 12.8197, 12.5526}),
		new Species("Co+++",		"Co+++",		3,	8,		58.9332,	new SpeciesReference[] {
			new SpeciesReference("Co++", 1),
			new SpeciesReference(" H+", 1),
			new SpeciesReference(" H2O", -0.5),
			new SpeciesReference(" O2(aq)", 0.25),
			}, new double[] {11.6818, 11.505, 11.3166, 11.1532, 11.0114, 10.9134, 10.8659, 10.8368}),
		new Species("Cr++",			"Cr++",			2,	5,		51.996,		new SpeciesReference[] {
			new SpeciesReference("H+", -1),
			new SpeciesReference(" H2O", 0.5),
			new SpeciesReference(" Cr+++", 1),
			new SpeciesReference(" O2(aq)", -0.25),
			}, new double[] {33.6814, 29.9291, 25.6126, 21.6721, 17.7896, 14.7267, 12.2289, 10.1676}),
		new Species("CrO4--",		"CrO4--",		-2,	4,		115.9936,	new SpeciesReference[] {
			new SpeciesReference("H+", -5),
			new SpeciesReference(" Cr+++", 1),
			new SpeciesReference(" H2O", 2.5),
			new SpeciesReference(" O2(aq)", 0.75),
			}, new double[] {9.7521, 8.3184, 6.9856, 6.0427, 5.4219, 5.2222, 5.3417, 5.6196}),
		new Species("CrO4---",		"CrO4---",		-3,	4,		115.9936,	new SpeciesReference[] {
			new SpeciesReference("H+", -6),
			new SpeciesReference(" Cr+++", 1),
			new SpeciesReference(" H2O", 3),
			new SpeciesReference(" O2(aq)", 0.5),
			}, new double[] {28.178, 28.059, 500, 500, 500, 500, 500, 500}),
		new Species("Cu++",			"Cu++",			2,	6,		63.546,		new SpeciesReference[] {
			new SpeciesReference("Cu+", 1),
			new SpeciesReference(" H2O", -0.5),
			new SpeciesReference(" H+", 1),
			new SpeciesReference(" O2(aq)", 0.25),
			}, new double[] {-21.1083, -18.787, -16.0739, -13.56, -11.0305, -8.971, -7.1974, -5.5541}),
		new Species("Eu++",			"Eu++",			2,	5,		151.96,		new SpeciesReference[] {
			new SpeciesReference("H+", -1),
			new SpeciesReference(" H2O", 0.5),
			new SpeciesReference(" Eu+++", 1),
			new SpeciesReference(" O2(aq)", -0.25),
			}, new double[] {30.8834, 27.4076, 23.4068, 19.7511, 16.1441, 13.2944, 10.9651, 9.0394}),
		new Species("Fe+++",		"Fe+++",		3,	9,		55.847,		new SpeciesReference[] {
			new SpeciesReference("H2O", -0.5),
			new SpeciesReference(" Fe++", 1),
			new SpeciesReference(" H+", 1),
			new SpeciesReference(" O2(aq)", 0.25),
			}, new double[] {-10.0553, -8.4878, -6.6954, -5.0568, -3.4154, -2.0747, -0.8908, 0.2679}),
		new Species("H2(aq)",		"H2(aq)",		0,	-0.5,	2.0158,		new SpeciesReference[] {
			new SpeciesReference("H2O", 1),
			new SpeciesReference(" O2(aq)", -0.5),
			}, new double[] {50.4955, 46.1129, 40.9736, 36.2002, 31.4328, 27.5804, 24.2906, 21.4524}),
		new Species("Hg2++",		"Hg2++",		2,	4,		401.18,		new SpeciesReference[] {
			new SpeciesReference("Hg++", 2),
			new SpeciesReference(" H2O", 1),
			new SpeciesReference(" H+", -2),
			new SpeciesReference(" O2(aq)", -0.5),
			}, new double[] {13.8942, 12.2214, 10.2919, 8.6703, 7.2584, 6.2764, 5.4907, 4.6769}),
		new Species("HS-",			"HS-",			-1,	3.5,	33.0679,	new SpeciesReference[] {
			new SpeciesReference("SO4--", 1),
			new SpeciesReference(" H+", 1),
			new SpeciesReference(" O2(aq)", -2),
			}, new double[] {152.1174, 138.3443, 122.1842, 107.1558, 91.9624, 79.6191, 69.1691, 60.1261}),
		new Species("MnO4-",		"MnO4-",		-1,	4,		118.9356,	new SpeciesReference[] {
			new SpeciesReference("H+", -3),
			new SpeciesReference(" Mn++", 1),
			new SpeciesReference(" H2O", 1.5),
			new SpeciesReference(" O2(aq)", 1.25),
			}, new double[] {22.343, 20.2928, 18.1686, 16.4473, 15.0554, 14.1826, 13.663, 13.1744}),
		new Species("MnO4--",		"MnO4--",		-2,	4,		118.9356,	new SpeciesReference[] {
			new SpeciesReference("H+", -4),
			new SpeciesReference(" Mn++", 1),
			new SpeciesReference(" H2O", 2),
			new SpeciesReference(" O2(aq)", 1),
			}, new double[] {34.924, 32.4103, 29.8154, 27.7303, 26.0159, 24.9467, 24.3264, 23.8178}),
		new Species("N2(aq)",		"N2(aq)",		0,	3,		28.0134,	new SpeciesReference[] {
			new SpeciesReference("H2O", -1),
			new SpeciesReference(" H+", 2),
			new SpeciesReference(" NO3-", 2),
			new SpeciesReference(" O2(aq)", -2.5),
			}, new double[] {8.8995, 7.7393, 5.841, 3.5667, 0.7156, -2.1191, -4.9973, -8.1171}),
		new Species("NH4+",			"NH4+",			1,	2.5,	18.0383,	new SpeciesReference[] {
			new SpeciesReference("NO3-", 1),
			new SpeciesReference(" H+", 2),
			new SpeciesReference(" H2O", 1),
			new SpeciesReference(" O2(aq)", -2),
			}, new double[] {58.2003, 52.9326, 46.5866, 40.5545, 34.2601, 28.9895, 24.3387, 20.2251}),
		new Species("NO2-",			"NO2-",			-1,	3,		46.0055,	new SpeciesReference[] {
			new SpeciesReference("NO3-", 1),
			new SpeciesReference(" O2(aq)", -0.5),
			}, new double[] {16.8517, 15.3132, 13.5245, 11.8941, 10.2613, 8.9505, 7.8182, 6.758}),
		new Species("Np+++",		"Np+++",		3,	8,		237.0482,	new SpeciesReference[] {
			new SpeciesReference("Np++++", 1),
			new SpeciesReference(" H+", -1),
			new SpeciesReference(" H2O", 0.5),
			new SpeciesReference(" O2(aq)", -0.25),
			}, new double[] {21.7175, 19.0149, 15.9014, 13.0557, 10.2453, 8.0231, 6.2012, 4.6938}),
		new Species("NpO2+",		"NpO2+",		1,	4,		269.047,	new SpeciesReference[] {
			new SpeciesReference("Np++++", 1),
			new SpeciesReference(" H+", -3),
			new SpeciesReference(" H2O", 1.5),
			new SpeciesReference(" O2(aq)", 0.25),
			}, new double[] {-10.407, -10.5915, -10.7528, -10.9024, -11.0607, -11.2011, -11.3445, -11.5154}),
		new Species("NpO2++",		"NpO2++",		2,	5,		269.047,	new SpeciesReference[] {
			new SpeciesReference("Np++++", 1),
			new SpeciesReference(" H+", -2),
			new SpeciesReference(" H2O", 1),
			new SpeciesReference(" O2(aq)", 0.5),
			}, new double[] {-11.3729, -11.2085, -10.9337, -10.6341, -10.2702, -9.9302, -9.6042, -9.3329}),
		new Species("Pu+++",		"Pu+++",		3,	8,		244,		new SpeciesReference[] {
			new SpeciesReference("H2O", -0.5),
			new SpeciesReference(" PuO2++", 1),
			new SpeciesReference(" H+", 1),
			new SpeciesReference(" O2(aq)", -0.75),
			}, new double[] {13.8329, 12.6244, 11.1389, 9.7287, 8.2586, 7.0377, 5.9718, 5.084}),
		new Species("Pu++++",		"Pu++++",		4,	11,		244,		new SpeciesReference[] {
			new SpeciesReference("H2O", -1),
			new SpeciesReference(" PuO2++", 1),
			new SpeciesReference(" H+", 2),
			new SpeciesReference(" O2(aq)", -0.5),
			}, new double[] {8.1113, 8.2392, 8.2993, 8.3039, 8.2372, 8.1298, 7.9901, 7.8711}),
		new Species("PuO2+",		"PuO2+",		1,	4,		275.9988,	new SpeciesReference[] {
			new SpeciesReference("H+", -1),
			new SpeciesReference(" PuO2++", 1),
			new SpeciesReference(" H2O", 0.5),
			new SpeciesReference(" O2(aq)", -0.25),
			}, new double[] {6.1012, 5.3396, 4.429, 3.5447, 2.592, 1.7698, 1.0219, 0.3479}),
		new Species("Ru(OH)2++",	"Ru(OH)2++",	2,	5,		135.0846,	new SpeciesReference[] {
			new SpeciesReference("Ru+++", 1),
			new SpeciesReference(" H2O", 1.5),
			new SpeciesReference(" H+", -1),
			new SpeciesReference(" O2(aq)", 0.25),
			}, new double[] {-7.5341, -7.5936, 500, 500, 500, 500, 500, 500}),
		new Species("Ru++",			"Ru++",			2,	5,		101.07,		new SpeciesReference[] {
			new SpeciesReference("H+", -1),
			new SpeciesReference(" Ru+++", 1),
			new SpeciesReference(" H2O", 0.5),
			new SpeciesReference(" O2(aq)", -0.25),
			}, new double[] {17.4089, 17.4684, 500, 500, 500, 500, 500, 500}),
		new Species("RuO4",			"RuO4",			0,	4,		165.0676,	new SpeciesReference[] {
			new SpeciesReference("H+", -3),
			new SpeciesReference(" Ru+++", 1),
			new SpeciesReference(" H2O", 1.5),
			new SpeciesReference(" O2(aq)", 1.25),
			}, new double[] {500, 1.3819, 500, 500, 500, 500, 500, 500}),
		new Species("RuO4-",		"RuO4-",		-1,	4,		165.0676,	new SpeciesReference[] {
			new SpeciesReference("H+", -4),
			new SpeciesReference(" Ru+++", 1),
			new SpeciesReference(" H2O", 2),
			new SpeciesReference(" O2(aq)", 1),
			}, new double[] {500, 6.0229, 500, 500, 500, 500, 500, 500}),
		new Species("RuO4--",		"RuO4--",		-2,	4,		165.0676,	new SpeciesReference[] {
			new SpeciesReference("H+", -5),
			new SpeciesReference(" Ru+++", 1),
			new SpeciesReference(" H2O", 2.5),
			new SpeciesReference(" O2(aq)", 0.75),
			}, new double[] {500, 17.6273, 500, 500, 500, 500, 500, 500}),
		new Species("Se--",			"Se--",			-2,	4,		78.96,		new SpeciesReference[] {
			new SpeciesReference("SeO3--", 1),
			new SpeciesReference(" O2(aq)", -1.5),
			}, new double[] {100.5599, 91.784, 81.3882, 71.6121, 61.6359, 53.4528, 46.4588, 40.4879}),
		new Species("SeO4--",		"SeO4--",		-2,	4,		142.9576,	new SpeciesReference[] {
			new SpeciesReference("SeO3--", 1),
			new SpeciesReference(" O2(aq)", 0.5),
			}, new double[] {-15.3086, -13.9755, -12.4218, -11.0093, -9.6032, -8.4962, -7.5905, -6.8714}),
		new Species("Sn++",			"Sn++",			2,	6,		118.69,		new SpeciesReference[] {
			new SpeciesReference("Sn++++", 1),
			new SpeciesReference(" H+", -2),
			new SpeciesReference(" H2O", 1),
			new SpeciesReference(" O2(aq)", -0.5),
			}, new double[] {41.4508, 37.6193, 33.1431, 28.9392, 24.6209, 21.0581, 17.9962, 15.3404}),
		new Species("Tc+++",		"Tc+++",		3,	8,		97.000,		new SpeciesReference[] {
			new SpeciesReference("TcO4-", 1),
			new SpeciesReference(" H2O", -2),
			new SpeciesReference(" H+", 4),
			new SpeciesReference(" O2(aq)", -1),
			}, new double[] {47.3936, 47.6316, 500, 500, 500, 500, 500, 500}),
		new Species("TcO++",		"TcO++",		2,	5,		112.9994,	new SpeciesReference[] {
			new SpeciesReference("H+", 3),
			new SpeciesReference(" TcO4-", 1),
			new SpeciesReference(" H2O", -1.5),
			new SpeciesReference(" O2(aq)", -0.75),
			}, new double[] {31.3378, 31.5163, 500, 500, 500, 500, 500, 500}),
		new Species("TcO4--",		"TcO4--",		-2,	4,		160.9976,	new SpeciesReference[] {
			new SpeciesReference("H+", -1),
			new SpeciesReference(" TcO4-", 1),
			new SpeciesReference(" H2O", 0.5),
			new SpeciesReference(" O2(aq)", -0.25),
			}, new double[] {31.7753, 31.8348, 500, 500, 500, 500, 500, 500}),
		new Species("TcO4---",		"TcO4---",		-3,	4,		160.9976,	new SpeciesReference[] {
			new SpeciesReference("H+", -2),
			new SpeciesReference(" TcO4-", 1),
			new SpeciesReference(" H2O", 1),
			new SpeciesReference(" O2(aq)", -0.5),
			}, new double[] {63.1841, 63.3031, 500, 500, 500, 500, 500, 500}),
		new Species("U+++",			"U+++",			3,	8,		238.029,	new SpeciesReference[] {
			new SpeciesReference("H+", -1),
			new SpeciesReference(" U++++", 1),
			new SpeciesReference(" H2O", 0.5),
			new SpeciesReference(" O2(aq)", -0.25),
			}, new double[] {34.2341, 30.3571, 25.9004, 21.8377, 17.8434, 14.7001, 12.1441, 10.0409}),
		new Species("UO2+",			"UO2+",			1,	4,		270.0278,	new SpeciesReference[] {
			new SpeciesReference("H+", -3),
			new SpeciesReference(" U++++", 1),
			new SpeciesReference(" H2O", 1.5),
			new SpeciesReference(" O2(aq)", 0.25),
			}, new double[] {-15.1986, -15.07, -14.8654, -14.6675, -14.4645, -14.3027, -14.1862, -14.1285}),
		new Species("UO2++",		"UO2++",		2,	5,		270.0278,	new SpeciesReference[] {
			new SpeciesReference("H+", -2),
			new SpeciesReference(" U++++", 1),
			new SpeciesReference(" H2O", 1),
			new SpeciesReference(" O2(aq)", 0.5),
			}, new double[] {-35.9229, -33.7844, -31.2373, -28.8495, -26.412, -24.4197, -22.7423, -21.3414}),
		new Species("VO++",			"VO++",			2,	5,		66.9408,	new SpeciesReference[] {
			new SpeciesReference("H+", -1),
			new SpeciesReference(" V+++", 1),
			new SpeciesReference(" H2O", 0.5),
			new SpeciesReference(" O2(aq)", 0.25),
			}, new double[] {-16.9795, -15.7195, -14.2392, -12.8813, -11.537, -10.4738, -9.6096, -8.9145}),
		new Species("VO4---",		"VO4---",		-3,	4,		114.939,	new SpeciesReference[] {
			new SpeciesReference("H+", -6),
			new SpeciesReference(" V+++", 1),
			new SpeciesReference(" H2O", 3),
			new SpeciesReference(" O2(aq)", 0.5),
			}, new double[] {8.0598, 8.1218, 8.6412, 9.5429, 10.8566, 12.3333, 13.9711, 15.6531}),
	};

	//I am not hardcoding past this point for this version of EXcalibur
}

using System;
using System.Collections.Generic;
using System.Collections;

public static class EightLetter
{
    #region Definition
    public static List<int> eightList = new List<int>
    { 17,24,25,26,28,29,30,31,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,176,177,178,179,180,181,182,183,184,185,186,187,188,189,190,191,192,193,194,195,196,197,198,199,200,201,202,203,204,205,206,207,208,209,210,211,212,213,214,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238,239,240,241,242,243,244,245,246,247,248,249,250,251,252,253,254,255,256,257,258,259,260,261,262,263,264,265,266,267,268,269,270,271,272,273,274,275,276,277,278,279,280,281,282,283,284,285,286,287,288,289,290,291,292,293,294,295,296,297,298,299,300,301,302,303,304,305,306,307,308,309,310,311,312,313,314,315,316,317,318,319,320,321,322,323,324,325,326,327,328,329,330,331,332,333,334,335,336,337,338,339,340,341,342,343,344,345,346,347,348,349,350,351,352,353,354,355,356,357,358,359,360,361,362,363,364,365,366,367,368,369,370,371,372,373,374,375,376,377,378,379,380,381,382,383,384,385,386,387,388,389,390,391,392,393,394,395,396,397,398,399,400,401,402,403,404,405,406,407,408,409,410,411,412,413,414,415,416,417,418,419,420,421,422,423,424,425,426,427,428,429,430,431,432,433,434,435,436,437,438,439,440,441,442,443,444,445,446,447,448,449,450,451,452,453,454,455,456,457,458,459,460,461,462,463,464,465,466,467,468,469,470,471,472,473,474,475,476,477,478,479,480,481,482,483,484,485,486,487,488,489,490,491,492,493,494,495,496,497,498,499,500,501,502,503,504,505,506,507,508,509,510,511,512,513,514,515,516,517,518,519,520,521,522,523,524,525,526,527,528,529,530,531,532,533,534,535,536,537,538,539,540,541,542,543,544,545,546,547,548,549,550,551,552,553,554,555,556,557,558,559,560,561,562,563,564,565,566,567,568,569,570,571,572,573,574,575,576,577,578,579,580,581,582,583,584,585,586,587,588,589,590,591,592,593,594,595,596,597,598,599,600,601,602,603,604,605,606,607,608,609,610,611,612,613,614,615,616,617,618,619,620,621,622,623,625,626,627,628,629,630,631,632,633,634,635,636,637,638,639,640,641,642,643,644,645,646,647,648,649,650,651,652,653,654,655,656,657,658,659,660,661,662,663,664,665,666,668,669,670,671,672,673,674,675,676,677,678,679,680,681,682,683,684,685,686,687,688,689,690,691,692,693,694,695,696,697,698,699,700,701,702,703,704,705,706,707,708,709,710,711,712,713,714,716,717,718,719,720,721,722,723,724,725,726,727,728,729,730,731,732,733,734,735,736,737,738,740,742,743,744,745,748,749,750,751,752,753,754,755,756,757,760,761,762,763,764,765,766,767,768,769,770,771,773,774,775,776,777,778,781,782,784,785,786,787,788,789,790,791,792,793,794,795,796,797,798,799,800,801,802,803,804,805,806,807,808,810,811,813,814,815,817,818,820,821,822,823,824,825,826,828,829,830,832,833,834,835,836,837,840,841,842,843,844,846,847,848,849,852,855,859,863,864,869,871,872,873,875,876,877,878,879,880,881,884,885,886,887,888,890,892,897,898,900,901,902,903,904,905,906,907,908,911,912,913,914,915,918,921,922,924,926,928,929,930,932,933,934,936,937,939,940,941,946,947,948,949,952,953,954,955,959,960,962,964,965,966,967,968,969,970,972,974,976,979,982,988,989,990,991,994,995,997,999,1000,1001,1004,1005,1006,1008,1013,1014,1015,1017,1024,1027,1030,1036,1041,1042,1043,1044,1048,1051,1054,1063,1066,1067,1068,1071,1073,1076,1079,1082,1085,1091,1093,1104,1108,1112,1114,1115,1116,1117,1122,1133,1142,1146,1149,1152,1166,1175,1184,1187,1193,1196,1208,1224,1225,1234,1242,1244,1245,1259,1283,1285,1298,1307,1319,1326,1333,1354,1359,1365,1367,1375,1379,1386,1453,1461 };
    public static int[,] eightArray = new int[965, 2]
        { {1,0},{1,2},{1,3},{1,1},{1,6},{3,4},{2,5},{1,10},{3,11},{2,14},{3,15},{1,17},{1,23},{3,20},{1,21},{8,22},{4,32},{3,34},{2,38},{2,40},{4,42},{2,48},{1,53},{4,46},{2,56},{1,61},{4,54},{3,57},{4,64},{2,68},{8,70},{3,80},{2,84},{5,72},{5,81},{5,91},{8,93},{4,97},{5,100},{5,107},{4,115},{9,117},{7,112},{5,123},{10,130},{9,131},{8,162},{9,147},{6,167},{14,170},{2,194},{8,183},{9,185},{11,209},{11,180},{8,206},{10,225},{9,213},{8,232},{14,239},{8,269},{9,279},{7,255},{7,271},{9,310},{13,300},{6,308},{16,312},{7,320},{14,356},{12,329},{9,353},{10,340},{11,344},{9,421},{15,397},{10,403},{8,427},{13,429},{10,415},{10,407},{14,463},{17,468},{8,491},{18,466},{15,488},{21,495},{15,530},{5,558},{6,599},{17,547},{16,556},{17,616},{18,594},{13,662},{17,613},{13,615},{12,603},{11,666},{16,677},{12,740},{15,715},{18,720},{24,741},{20,742},{22,784},{14,704},{24,761},{15,857},{10,834},{24,841},{21,861},{17,899},{14,915},{24,926},{8,860},{11,978},{14,1029},{22,997},{17,1005},{14,1017},{26,957},{14,1037},{16,1074},{18,1151},{20,1087},{21,1075},{16,1081},{14,1142},{12,1153},{20,1136},{12,1161},{18,1197},{17,1160},{22,1174},{18,1119},{12,1254},{20,1308},{18,1281},{18,1286},{15,1356},{20,1398},{17,1408},{19,1393},{22,1402},{17,1438},{19,1454},{16,1464},{16,1506},{16,1450},{19,1537},{16,1550},{13,1495},{18,1591},{17,1610},{11,1635},{23,1602},{17,1627},{17,1634},{17,1712},{25,1599},{15,1736},{19,1707},{23,1727},{12,1806},{17,1751},{19,1794},{20,1840},{23,1811},{17,1869},{15,1897},{15,1965},{22,1895},{15,1908},{18,1902},{22,1894},{14,2058},{19,2003},{24,1804},{22,2043},{24,1955},{16,1978},{13,2047},{21,2048},{19,2116},{26,2187},{14,2135},{23,2124},{12,2218},{19,2180},{6,2222},{17,2227},{21,2263},{19,2166},{25,2252},{22,2188},{19,2358},{11,2327},{19,2370},{26,2346},{19,2302},{26,2369},{15,2345},{21,2464},{19,2411},{19,2437},{21,2475},{18,2583},{28,2484},{29,2569},{16,2456},{14,2575},{21,2658},{21,2630},{12,2686},{23,2681},{21,2604},{21,2678},{23,2802},{13,2554},{22,2766},{17,2872},{21,2603},{18,2693},{18,2852},{20,2816},{15,2734},{23,2906},{18,2844},{15,2982},{27,2841},{17,2990},{20,2973},{16,3047},{15,3070},{17,3014},{20,3124},{18,3143},{23,3187},{21,3093},{20,3089},{17,3069},{11,3229},{15,3234},{17,3244},{23,3206},{20,3307},{15,3320},{20,3295},{14,3339},{9,3371},{15,3328},{7,3380},{9,3479},{16,3429},{19,3302},{16,3309},{17,3519},{19,3469},{18,3502},{27,3503},{16,3528},{16,3510},{13,3558},{13,3582},{17,3487},{19,3475},{19,3570},{18,3513},{22,3595},{16,3618},{17,3534},{21,3664},{19,3637},{16,3685},{14,3693},{21,3763},{11,3776},{22,3753},{19,3791},{25,3731},{19,3843},{24,3884},{16,3895},{15,3948},{20,3850},{11,4004},{15,3981},{11,3994},{5,4028},{14,3952},{20,3875},{17,4013},{15,4105},{16,3966},{19,4046},{16,4154},{16,4040},{18,4076},{13,4091},{8,4115},{22,4109},{15,4210},{21,4194},{23,4253},{16,4262},{16,4275},{17,4202},{19,4267},{16,4246},{19,4255},{15,4388},{9,4384},{15,4406},{12,4300},{16,4460},{13,4403},{12,4414},{11,4421},{15,4465},{8,4474},{11,4609},{26,4341},{14,4497},{14,4480},{10,4598},{11,4464},{11,4511},{12,4553},{10,4612},{8,4655},{13,4603},{7,4688},{13,4664},{13,4676},{13,4651},{10,4665},{16,4811},{18,4714},{9,4787},{13,4734},{11,4710},{14,4810},{15,4793},{15,4868},{12,4785},{15,4874},{11,4825},{16,4836},{15,4860},{13,4842},{17,4748},{10,4937},{15,4864},{13,4962},{13,4931},{11,4916},{11,4946},{12,4936},{12,4981},{6,5024},{8,5006},{7,5084},{12,4994},{8,5122},{12,5162},{19,5090},{13,5116},{12,5068},{11,5137},{10,5140},{13,4956},{14,5133},{12,5224},{6,5181},{13,5200},{13,5203},{12,5179},{9,5282},{6,5121},{11,5313},{14,5244},{12,5286},{8,5258},{10,5199},{10,5309},{12,5294},{8,5302},{10,5326},{10,5441},{10,5423},{17,5397},{5,5474},{8,5365},{10,5345},{15,5354},{11,5337},{16,5398},{7,5540},{9,5409},{11,5484},{15,5455},{10,5396},{15,5557},{10,5439},{8,5505},{13,5525},{13,5580},{7,5611},{6,5544},{10,5489},{14,5510},{8,5571},{17,5627},{14,5658},{16,5644},{11,5652},{11,5587},{12,5618},{14,5549},{9,5509},{10,5678},{6,5752},{11,5686},{8,5784},{14,5817},{13,5687},{5,5809},{10,5769},{5,5848},{8,5787},{4,5930},{10,5721},{6,5744},{4,5813},{15,5831},{9,5764},{8,5913},{8,5846},{5,5865},{7,5896},{6,5918},{9,5943},{5,5952},{11,5861},{8,5837},{13,5903},{6,5938},{11,5907},{4,6022},{9,5972},{8,6005},{5,6076},{9,6016},{8,5945},{5,5934},{12,6000},{8,6098},{8,5969},{11,5989},{1,6112},{5,6102},{1,6138},{10,6072},{8,6116},{12,6089},{5,6125},{16,6077},{7,6081},{7,6063},{3,6166},{9,6198},{14,6069},{3,6151},{9,6095},{12,6126},{8,6219},{6,6170},{8,6175},{5,6157},{3,6196},{8,6189},{9,6194},{3,6254},{8,6234},{10,6218},{6,6252},{6,6315},{2,6271},{7,6274},{13,6303},{13,6241},{5,6278},{8,6345},{1,6350},{6,6370},{7,6344},{7,6347},{10,6310},{8,6341},{4,6379},{10,6330},{6,6451},{6,6394},{7,6332},{5,6357},{8,6349},{6,6421},{3,6461},{9,6426},{7,6383},{6,6437},{7,6400},{3,6485},{3,6433},{3,6405},{6,6445},{5,6532},{7,6455},{6,6525},{7,6480},{11,6454},{5,6578},{10,6513},{2,6561},{6,6436},{3,6521},{9,6497},{9,6552},{4,6555},{9,6613},{7,6566},{6,6587},{3,6536},{7,6554},{5,6586},{6,6624},{10,6595},{6,6618},{6,6611},{4,6615},{3,6653},{4,6650},{4,6663},{5,6695},{2,6689},{3,6608},{4,6684},{3,6697},{4,6727},{9,6620},{4,6651},{4,6723},{7,6649},{7,6688},{7,6656},{7,6635},{4,6732},{3,6735},{10,6724},{2,6787},{5,6728},{5,6763},{2,6751},{5,6722},{4,6780},{7,6731},{3,6805},{5,6753},{7,6757},{3,6776},{4,6746},{5,6774},{2,6770},{5,6812},{7,6784},{7,6809},{1,6853},{4,6849},{1,6908},{2,6845},{6,6850},{6,6837},{4,6804},{4,6914},{5,6839},{6,6868},{2,6855},{2,6864},{3,6882},{8,6840},{4,6906},{5,6893},{7,6858},{3,6951},{2,6931},{5,6896},{9,6872},{7,6891},{3,6889},{2,7013},{3,6898},{6,6909},{6,6970},{5,6922},{2,7015},{4,6969},{4,6994},{3,6984},{2,7011},{4,6957},{6,7003},{2,6902},{4,6904},{6,6960},{3,6952},{4,6999},{6,6983},{2,7090},{4,7000},{2,7047},{2,6965},{6,7006},{4,7038},{2,7065},{4,6998},{2,7094},{4,7069},{7,7026},{4,7037},{5,7051},{7,7028},{1,7062},{1,7098},{2,7083},{3,6991},{3,7068},{2,7057},{1,7035},{2,7130},{2,7149},{2,7031},{2,7121},{3,7093},{5,7095},{2,7124},{5,7064},{1,7087},{5,7099},{2,7110},{3,7111},{1,7189},{7,7108},{13,7088},{4,7148},{4,7106},{2,7188},{4,7143},{1,7223},{6,7155},{5,7144},{2,7152},{7,7122},{5,7107},{2,7184},{4,7147},{3,7164},{2,7142},{3,7133},{2,7170},{2,7165},{2,7238},{2,7197},{4,7182},{2,7201},{4,7196},{5,7172},{1,7237},{1,7271},{5,7221},{2,7259},{6,7185},{3,7183},{4,7227},{4,7245},{4,7251},{2,7253},{4,7228},{1,7232},{2,7263},{1,7230},{3,7226},{1,7262},{3,7265},{3,7261},{2,7294},{1,7266},{4,7267},{6,7273},{3,7254},{3,7315},{3,7286},{1,7340},{1,7313},{2,7281},{2,7257},{4,7292},{1,7252},{3,7298},{4,7306},{4,7278},{3,7299},{5,7316},{2,7303},{2,7320},{1,7350},{1,7333},{1,7389},{2,7369},{2,7329},{1,7351},{3,7321},{3,7359},{2,7363},{2,7337},{1,7311},{3,7373},{4,7327},{5,7353},{3,7322},{4,7356},{2,7346},{3,7405},{6,7357},{2,7354},{4,7372},{2,7343},{1,7426},{4,7376},{4,7366},{2,7412},{1,7433},{4,7390},{1,7419},{1,7383},{1,7396},{3,7432},{2,7409},{3,7399},{1,7418},{1,7452},{8,7391},{2,7440},{2,7434},{2,7417},{5,7382},{1,7422},{1,7479},{1,7483},{1,7453},{1,7491},{2,7441},{2,7428},{2,7474},{3,7437},{3,7478},{2,7457},{3,7445},{4,7436},{2,7448},{1,7471},{2,7484},{2,7464},{2,7461},{1,7493},{1,7470},{1,7486},{3,7469},{1,7508},{5,7459},{3,7503},{1,7467},{1,7490},{5,7463},{1,7518},{2,7494},{3,7492},{1,7522},{3,7510},{2,7515},{3,7488},{1,7497},{2,7505},{1,7517},{1,7509},{2,7528},{1,7545},{4,7514},{2,7527},{1,7526},{2,7520},{1,7530},{1,7548},{1,7552},{1,7540},{1,7532},{1,7547},{2,7541},{1,7544},{1,7534},{1,7554},{2,7549},{3,7537},{1,7558},{1,7561},{1,7562},{2,7539},{1,7557},{1,7555},{1,7576},{1,7559},{1,7573},{1,7567},{1,7560},{2,7553},{2,7565},{2,7574},{2,7578},{1,7564},{1,7582},{1,7568},{1,7588},{1,7572},{1,7592},{1,7593},{2,7586},{4,7570},{1,7589},{1,7571},{2,7596},{3,7581},{1,7609},{2,7566},{1,7585},{2,7605},{1,7594},{1,7591},{2,7598},{2,7615},{1,7606},{2,7590},{1,7604},{1,7614},{1,7607},{1,7600},{2,7595},{1,7627},{4,7611},{2,7610},{1,7603},{1,7618},{1,7626},{3,7624},{1,7640},{1,7635},{1,7633},{1,7629},{1,7644},{1,7636},{1,7641},{2,7617},{3,7619},{2,7631},{1,7623},{3,7634},{2,7638},{1,7659},{2,7649},{1,7639},{1,7658},{1,7653},{1,7661},{1,7662},{2,7645},{4,7650},{1,7676},{1,7648},{1,7675},{1,7674},{1,7669},{2,7663},{1,7666},{1,7664},{1,7651},{1,7672},{1,7668},{1,7670},{2,7673},{1,7667},{1,7678},{1,7681},{2,7680},{1,7684},{1,7687},{1,7679},{1,7686},{2,7688},{1,7691},{2,7682},{1,7692},{1,7689},{1,7694},{1,7693},{1,7701},{1,7695},{1,7704},{1,7698},{1,7696},{1,7700},{1,7699},{1,7703},{1,7697},{1,7705},{1,7702},{1,7709},{2,7706},{1,7711},{1,7712},{2,7708},{1,7707},{1,7715},{1,7714},{1,7719},{1,7717},{2,7716},{1,7721},{1,7718},{2,7722},{1,7723},{1,7724},{1,7726},{1,7728},{1,7727},{1,7730},{1,7729},{1,7731},{1,7734},{1,7733},{1,7732},{1,7736},{1,7735},{1,7737},{1,7738},{1,7739},{1,7741},{1,7740},{1,7743},{1,7742},{1,7746},{1,7745},{1,7744},{1,7748},{1,7747},{1,7749},{1,7750} };
    #endregion
    
    #
}
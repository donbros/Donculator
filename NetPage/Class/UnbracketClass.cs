using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using Extreme.Mathematics.Curves;
using System.Text.RegularExpressions;

namespace Calculator
{

    // Papildomi komentarai:
    // imame tik tai kad pirmo lygio skliaustuose ir daugybos/dalybos ženklą, ignoruojame pliusą,
    // minusą prisaikdiname prie skaičiaus


    /// <summary>
    /// Ši klasė atlieka visus reikalingus veiksmus "text" atskliaudimui
    /// </summary>
    public class UnbracketClass : SimpleArithmeticClass
    {

        /// <summary>
        /// Tikslas: atlikti reikiamus veiksmus norint atskliausti lygtį
        /// Rezultatas: atskliausta lygtis
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Unbracket(string text, out List<string> denominatorsList)
        {
            denominatorsList = new List<string>();
            while (text.Contains('(') && text.Contains(')'))
            {
                // ką gali apverčia (komiška algoritme, tai jog jis bilekiek minusų spamina
                for (int i = 0; i < text.Length; i++)
                {
                    text = text;

                    string subTextForDivision = "";

                    int indexas = i;

                    DivideTextToSubTextForDivisionSimplifier(indexas, text, ref subTextForDivision); // i nesikeičia

                    string subTextBeforeChanges2 = subTextForDivision;

                    subTextForDivision = SimplifyDivision(subTextForDivision); // ČIA SĖDI FUNKCIJA SUPRASTINANTI "/" iki 1/x arba 1/(x+1) be jokių x/a/b

                    text = text.Remove(indexas, subTextBeforeChanges2.Length).Insert(indexas, subTextForDivision);

                    i = subTextForDivision.Length + indexas;

                    i = i;
                }

                text = text;

                // ką gali atskliaudžia (šis algoritmas viską atskliaudžia tik negali atskliausti a/(b+c) arba a/(a*b) arba a/(a-b) arba a/(a/b)) - tuo rūpinasi prastinimo funkciją po abiejais algoritmais)
                for (int i = 0; i < text.Length; i++) // text.Length kis dinamiškai (tikėkimės nebus null)
                {
                    string simbolisPatikrinti = text[i].ToString(); // TESTAMS
                    // kaip nepamesti indekso vietos, jeigu deletinsim ir insertinsim
                    if (text[i] == '(')
                    {
                        // tiesiog pradedamas tikrinimas nuo grynos pradžios
                        Backtrack(ref i, text);

                        string dabartinistTekstas = text; // TESTAVIMUI

                        int iBeggining = i;

                        string subText = "";

                        DivideTextToSubText(ref i, text, ref subText); // subText - iki sumos ar skirtumo analizuojama teksto dalis su skliaustu

                        string subTextBeforeChanges = subText;

                        // gavome subText = 5*(-1)*(2*x+3);
                        // indeksas 'i' laukia pakeisti savo vietą priklausomai nuo to ką išsireikšime

                        // reikia dabar atlikti substringo išmanųjį išskaidymą
                        // kas tai yra?
                        // skaidymas, kuris sudeda šį substringą List<string> pagal kelis kriterijus:
                        // * dedamas skaičius su minuso ženklu, kad visi veiksmai būtų sudėtis
                        // * nuimami pirmo lygio skliaustai, tačiau jokiu būdu nelendama prie antro lygio skliaustų (reik tikrintojo)
                        // * su kuo teks atlikti daugyba aišku pagal tai jei tarp dviejų skaičių liste nėra "*" ar "/"
                        // * ignoruojami visi "(" kol nerandamas ")" viskas kas buvo ignoruota įmetama į lista pagal
                        // split('+') taisyklę

                        // PASVARSTYMUI galbūt sukišti visą skliaustą į listą, o paskaidyti "+" vėliau praplečiant listą arba įdedant į kitą

                        // PROBLEMA: išskaido vidinius '+'
                        // PAVYZDYS: (8+(2*x)*(1+x))*x -> išskaido į 8; (2*x)*(1; x)

                        // subText = SimplifyDivision(subText); // nereikalingas čia, jau įvykdytas metodas anksčiau

                        List<string> subTextList = SubTextDivider(subText); // tekstą suskaido į sąrašą, kurį lengvą išdalyti

                        List<List<string>> table = MultiplicationDivisionTable(subTextList);

                        string newString = "";

                        // Formuojant naują string sudėtingesni elementai 1/(x+1) gauna skliaustą, paprastesni - negauna
                        for (int z = 0; z < table.Count; z++)
                        {
                            // jei prieš / yra ir funkcija sudėtinga viduje
                            for (int v = 0; v < table[z].Count; v++)
                            {
                                if (v - 1 >= 0)
                                {
                                    bool arDalyba = (table[z][v - 1][0] == '/');
                                    if (arDalyba && // jei veiksmas dalyba ir po ja yra sudėtingas veiksmas (bet kas turintis +, -, *, /)
                                        (
                                        (table[z][v].Contains("+")) ||
                                        (table[z][v].Contains("-")) ||
                                        (table[z][v].Contains("*")) ||
                                        (table[z][v].Contains("/"))
                                        )
                                        )
                                    // Jei nėra nuogas skaičius. Biški per griežta,i nes dabar ir -x neįtrauks irgi, tačiau nereikia algoritmo rašyti
                                    // jei bus noro parašyti ateityje, bet man atrodo prastinimas čia sutaisys viską, jei bus gerai padarytas
                                    {
                                        char teiblas = table[z][v - 1][0];
                                        arDalyba = arDalyba;
                                        newString += "(" + table[z][v] + ")";
                                    }
                                    else
                                        newString += table[z][v];
                                }
                                else
                                {
                                    newString += table[z][v];
                                }
                            }
                            if (z != table.Count - 1)
                                newString += "+";
                        }

                        newString = operationSignsFixer1(newString);

                        // reikia sužinoti nuo kur trinam
                        text = text.Remove(iBeggining, subTextBeforeChanges.Length).Insert(iBeggining, newString);

                        // geras perkėlimas tikrinimui (tik nežinau ar reikia minus vieno ar ne, kol kas paliekame)
                        i = iBeggining + newString.Length - 1;
                        text = text;
                        i = i;
                        // ### pasiureti ar suveiks 2*()*2*()*2 ###

                        // *** darome daugyba t.y. suporuojam string'us*** 

                        // *** Senąjį text triname, naujajį įdedame ***

                        // *** tesiame ėjimą per string nuo insert galo (i = insertGalas ?) ***
                    }
                }

                text = CommonDenominatorSimplificationSearchAndFunctionSimplificaiton(text, ref denominatorsList);

                denominatorsList = denominatorsList;

                // } KAI PRAEINA VISĄ IR NEBELIEKA VIRŠUTINIŲ SKLIAUSTŲ VYKSTA PRASTINIMAS IR PO TO VĖL IŠ NAUJO VYKDOMAS CIKLAS TOLIAU NAIKINTI NAUJUS SKLIAUSTUS, KOL SKLIAUSTŲ NEBELIEKA

            }

            // ištrina perteklinį "+" pradžioje
            if (text[0] == '+')
            {
                text = text.Remove(0, 1);
            }

            return text;
        }


        /// <summary>
        /// subText
        /// </summary>
        /// <param name="i"></param>
        /// <param name="text"></param>
        /// <param name="subText"></param>
        private void DivideTextToSubTextForDivisionSimplifier(int i, string text, ref string subText)
        {
            subText = "";
            int indexBalance1 = 0;

            // gudriau ieskot su '(' index kaupikliu (if kaupiklis ignoruoja pliusa ir minusa kai jo nera iesko
            while (true)
            {
                if (i == text.Length)
                {
                    break;
                }
                if (indexBalance1 == 0)
                {
                    // jei 
                    // if ((text[i] == '+') || (text[i] == '-'))
                    // problema išmeta dar nieko neradus
                    if (((i - 1) >= 0))
                        if ((text[i] == '+') || (text[i] == '-' && text[i - 1] != '*' && text[i - 1] != '/'))
                        {
                            break;
                        }
                }
                if (text[i] == '(')
                {
                    indexBalance1++;
                }

                if (text[i] == ')')
                {
                    indexBalance1--;
                }
                subText += text[i];
                i++;
            }
        }

        /// <summary>
        /// Tikslas: supaprastinti dalybą, kad ją "suprastų" (būtų tinkamai paruošta) kombinatorinė daugyba
        /// Rezultatas: supaprastina dalyba iš a/b į a*1/b arba a/(a+b) į a*1/(a+b)
        /// </summary>
        /// <param name="subText"></param>
        /// <returns></returns>
        private string SimplifyDivision(string subText)
        {
            int indexBalance = 0;
            for (int i = 0; i < subText.Length; i++)
            {
                // apžvelgta situacija 1*(5+3/5)/3 - ją išsprendėme pridedami papildomą subText tikrintoją
                if (subText[i] == '(')
                {
                    indexBalance++;
                }
                if (subText[i] == ')') // turi žiūrėt vienu į priekį jeigu teigiamas, jei neigiamas skaičius du į priekį
                {
                    indexBalance--;
                }
                if (indexBalance == 0)
                {
                    // turi iškirpti "/b" ir perdaryti į "1/b"
                    if (subText[i] == '/')
                    {
                        // nuo i iki kitos veiksmo * ar / ar subText galo
                        int jBeginning = i; // nuo ko pradėti trynimą
                        int j = i + 1;
                        int miniIndexBalance = 0;
                        string partToInvert = "";
                        bool ended = false;
                        while (true)
                        {
                            // jei i+1 skliausto atsidarymas užsižymime ir įrašome iki skliausto uždarymo bei ženklo už jo
                            if (subText[j] == '(')
                            {
                                miniIndexBalance++;
                            }
                            if (subText[j] == ')') // turi žiūrėt vienu į priekį jeigu teigiamas, jei neigiamas skaičius du į priekį
                            {
                                miniIndexBalance--;
                            }
                            if (miniIndexBalance == 0)
                            {
                                if (subText[j] == '/' || subText[j] == '*')
                                {
                                    break;
                                }
                                // break prieš įrašant paskutinį ženklą
                                partToInvert += subText[j];
                            }
                            else
                                partToInvert += subText[j];
                            j++;
                            if (j == subText.Length)
                            {
                                ended = true;
                                break;
                            }
                        }
                        //int jEnd = j - 1; // nepaimame paskutinio ženklo, o jei ženklo nėra???
                        //if (ended)
                        int jEnd = j;

                        // * ateityje, jei nenorime, kad būtų "prispaminta" "1*1/" galime metodą padaryti išmanesnį
                        //   ir apversti tik antrą "/" iš eilės (atsižvelgiant į teigiamus ir neigiamus skaičius)
                        string invertedPart = "*1/" + partToInvert;
                        subText = subText;
                        subText = subText.Remove(jBeginning, jEnd - jBeginning).Insert(jBeginning, invertedPart); // j beggining įtraukiame, nes tai '/'

                        i = invertedPart.Length + jBeginning - 1; // reik -1 daryt nes jis iškart pakels
                        // nunulinam kad šiukšlių nebūtų
                        j = 0;
                        int ilgis = subText.Length;
                    }
                }
            }

            return subText;
        }

        /// <summary>
        /// sugrąžinamas indeksas iki išorės sumpos arba skirtumo (jį reik grąžinti, nes jis dažniausiai ieško "(")
        /// </summary>
        /// <param name="i"></param>
        /// <param name="text"></param>
        private void Backtrack(ref int i, string text)
        {
            // grįžtam iki "+" ar "-" ir tesiam

            while (true)
            {
                // sustadome kai pasiekiame '+' ar '-' arba indeksas tampa 0
                if ((i == 0)
                    || (text[i] == '+')
                    || (text[i] == '-' && (text[i - 1] != '*' && text[i - 1] != '/')) // minusas be daugybos prieš jį
                    )
                {
                    break;
                }
                i--;
            }
            // nuo sekančio skaičiaus kuris nėra "+" ar "-"
            // i ++;
        }


        /// <summary>
        /// išsireiškiame subText - viską nuo išorinio pliuso ar minuso iki kito išorinio pliuso ir minuso
        /// </summary>
        /// <param name="i"></param>
        /// <param name="text"></param>
        /// <param name="subText"></param>
        private void DivideTextToSubText(ref int i, string text, ref string subText)
        {
            subText = "";
            int indexBalance1 = 0;
            // randame dalį kurią manipuliuosime 
            bool neverFound = true;

            // gudriau ieskot su '(' index kaupikliu (if kaupiklis ignoruoja pliusa ir minusa kai jo nera iesko
            while (true)
            {
                if (i == text.Length)
                {
                    break;
                }
                if (indexBalance1 == 0)
                {
                    // jei 
                    // if ((text[i] == '+') || (text[i] == '-'))
                    // problema išmeta dar nieko neradus
                    if (((i - 1) >= 0))
                        if ((text[i] == '+') || (text[i] == '-' && text[i - 1] != '*' && text[i - 1] != '/'))
                        {
                            if (!neverFound)
                                break;
                        }
                }
                if (text[i] == '(')
                {
                    neverFound = false;
                    indexBalance1++;
                }

                if (text[i] == ')')
                {
                    indexBalance1--;
                }
                subText += text[i];
                i++;
            }
        }

        /// <summary>
        /// subText išskaidomas į daugiklių ir daliklių lentelę (visi išskyrus dalybą su skliaustais - tuo pasirūpins Denominator metod.)
        /// </summary>
        /// <param name="subText"></param>
        /// <returns></returns>
        private List<string> SubTextDivider(string subText)
        {

            string stringToPut = "";
            string miniStringToPut = "";
            int indexBalance = 0; // gal perkelti aukščiau ? 
            List<string> subTextList = new List<string>();

            bool entered = false;
            // sudėjimas į listą 

            bool putWithBrackets = false;

            for (int k = 0; k < subText.Length; k++)
            {
                if (subText[k] == '(')
                {
                    indexBalance++;
                }

                if (subText[k] == ')')
                {
                    indexBalance--;
                }

                if (subText[k] == '/')
                {
                    int indeksas = k;
                }

                // veiksmai atliekami priklausomai nuo indexBalance
                // kai 0 dedame į listą
                if (indexBalance == 0)
                {

                    //if (((subText[k] == '/') && (subText[k + 1] == '(')) || ((subText[k] == '/') && (subText[k + 1] == '-') && (subText[k + 2] == '(')))
                    //{
                    //    // kažkaip užžymėti, kad jo neskaidytų
                    //    stringToPut = stringToPut;
                    //    int indeksas = k;
                    //}

                    char sub = subText[k];
                    if (entered)
                    {
                        // kdaangi ką tik išėjome iš indexBalance paskaidyti atskiriant pagal '+' ir '-' (nu tuo kur visad, kur žiūri ar nėra daugybos)

                        if (!putWithBrackets)
                        {
                            miniStringToPut = "";
                            int miniIndexBalance = 0;
                            // pirma praleidžia pagalvok ką praleidžia
                            for (int j = 0; j < stringToPut.Length; j++)
                            {
                                // JEI PAMATO DALYBĄ APSKLIAUSTĄ IGNORUOJA
                                if (stringToPut[j] == '(')
                                {
                                    miniIndexBalance++;
                                }

                                if (stringToPut[j] == ')')
                                {
                                    miniIndexBalance--;
                                }
                                // jis tai daro bet tik ne tuo atveju kai būna skliausto viduje
                                if (miniIndexBalance == 0 && j != 0
                                    && ((stringToPut[j] == '+')
                                    || (stringToPut[j] == '-' && stringToPut[j - 1] != '*' && stringToPut[j - 1] != '/')))
                                {
                                    subTextList.Add(miniStringToPut);
                                    miniIndexBalance = miniIndexBalance;
                                    // indexBalance = indexBalance;
                                    miniStringToPut = "";
                                }
                                // buvo ELSE prieš tai, bet kai būna else, jis nepaima '-'
                                // išspręsta panaikinus else, o vėliau idėjus fixer'į, kad panaikintų perteklinius pliusus
                                miniStringToPut += stringToPut[j];
                                // arba iki kitų skliaustų
                            }

                            // eksperimentai
                            subTextList.Add(miniStringToPut);
                            miniStringToPut = "";

                            // jis man atrodo mėgsta pridėti šitą tuščią
                            stringToPut = "";

                            // PROBLEMA: pavojus jog nepaims galo skliausto


                            // *** Išmanus stringToPut skaidymas ***
                            // *** sudėjimas į listą stringToPut dalių ***
                        }
                        else
                        {
                            // subTextList.Add("(" + stringToPut + ")");
                            subTextList.Add(stringToPut);
                            putWithBrackets = false;
                            stringToPut = "";
                        }
                    }
                    else
                    {
                        if (subText[k] == '*' || subText[k] == '/') // pasiekiame * ar /
                        {
                            stringToPut = stringToPut;
                            char simbol = subText[k];
                            // PROBLEMA: ideda antra kart jeigu miniString jau idetas
                            // stringToPut = "" padariau anksciau ir dabar turetu neprideti
                            if (stringToPut != "")
                            {
                                subTextList.Add(stringToPut); // įdedam stringą
                                subTextList.Add(subText[k].ToString()); // įdedam ženklą
                                stringToPut = ""; // nunulinam stringą
                            }
                            else
                            {
                                subTextList.Add(subText[k].ToString()); // įdedam ženklą
                            }

                            // GRIEŽTAS PAŽYMĖJIMAS, kad neitų į skliaustus, jei prieš juos dalyba: visus su skliaustais, kur yra dalyba kelia nesiglinant ar ten yra veiksmų ar ne
                            if (((subText[k] == '/') && (subText[k + 1] == '(')) || ((subText[k] == '/') && (subText[k + 1] == '-') && (subText[k + 2] == '(')))
                            {
                                // kažkaip užžymėti, kad jo neskaidytų
                                stringToPut = stringToPut;
                                int indeksas = k;
                                putWithBrackets = true;
                            }

                        }
                        else
                        {
                            stringToPut += subText[k];
                        }
                        // dedam visus skaičius tarp daugbos ir dalybos į list
                    }
                    // nunulinam false, nes jau išėjo iš skliaustų ir atliko reikiamus veiksmus
                    entered = false;
                }
                // kol esame skliaustuose viską surašome į string'ą (kai >0)
                if (indexBalance >= 1)
                {
                    entered = true;

                    // pirmo skliausto ignorinimas
                    if (indexBalance == 1 && subText[k] == '(')
                    {
                        // stringToPut += subText[k + 1]; // jei k+1 paims paskutinį
                        // du kartu ta pati imeta del to skipiname
                    }
                    // nes kai pasikeis balance jau sekančio neignorinsim
                    else
                    {
                        stringToPut += subText[k];
                    }                              // jei k+1 paims paskutinį
                                                   // paskutinio skliausto neima nes laiku išeinama iš šios sąlygos
                                                   // įrašinėjam viską, kad po to galėtume skaidyti ir sudėti į listą
                }
            }
            // PROBLEMA: blogas skaidymas vidiniame cikle
            // PROBLEM 2: jei baigiasi ne ')' neima skaitmens

            miniStringToPut = miniStringToPut;
            stringToPut = stringToPut;
            if (miniStringToPut != "")
            {
                subTextList.Add(miniStringToPut);
            }
            else if (miniStringToPut == "")
            {
                if (stringToPut != "")
                {
                    subTextList.Add(stringToPut);
                }
            }

            // jei kažkas lieka (šiaip turėtų kažkur kitur būti, nes yra pavojus skliaustuose praleidinėti paskutiniu skaičius
            // nereikia kitur idėjau
            //if(miniStringToPut != "")
            //    subTextList.Add(miniStringToPut);

            return subTextList;
        }

        /// <summary>
        /// 
        /// Tikslas: atskliausti 1 lygio skliaustus naudojant kombinatorinę daugybą
        /// Principas 1: nebesirūpiname sudėtimi ir atimti, o tiesiog pažymime ar skaičius neigiamas ir teigiamas
        /// Principas 2: visos lenteles duomenis sudauginame vieną iš kito, pagal taisyklę: jei lentelėje tarp kelių narių nėra daugiklio (ar daliklio) reiškia šiuos skaičius dauginame kombinatoriniu principu
        /// Principas 3: dalyba nedauginama kombinatoriškai - tuo pasirūpina Denominator, kad visi daliniai taptų daugybomis, na ir tuomet šis metodas tuo pasirūpina
        /// Rezultatas: kelių lygių sąrašas (lentelė), kuria galima lengvai išsireikšti sąrašus "suklijuojant", o lygius sudedant - padarytas progresas atskliaudžiant "*"
        /// 
        /// </summary>
        /// <param name="subTextList"></param>
        /// <returns></returns>
        private List<List<string>> MultiplicationDivisionTable(List<string> subTextList)
        {
            // perašom List'ą į table List'ą
            List<List<string>> table = new List<List<string>>();
            List<string> row = subTextList;
            table.Add(row);

            bool Fixed = false;
            // būtent table.Count didės
            int j = 0;
            // raeliai kol j dar nepataisytas (t.y. x*x*x*x*x tol vykdome)
            while (!Fixed)
            // int j = 0; j < table.Count; j++
            {
                // nurodo, kuri lentelė yra didžiausia
                // idėja daliname didžiausias lenteles, nes jos garantuotai neišskaidytos
                // išskaidytos lentelės visada būna mažesnės
                // pradedam nuo didžiausios sumažinant duplikatų tikikimybę
                j = MaxIndex(table);
                //for (; j < table.Count; j++) // nežinau ar šito reikia
                //{
                // gal kuriam kokį temporary listą?
                // čia pažiūrėti ar nenujoja j !!!!!!!!!!!!!!
                for (int k = 0; k < table[j].Count; k++)
                {
                    string bla = table[j][k];
                    // apvžlegti atvejį kai tikrinamas "*" - taigi žinoma kad prieš jį nebus daugiklio
                    if ((k + 1 >= table[j].Count) || (table[j][k + 1] == "*" || table[j][k + 1] == "/"))
                    {
                        Fixed = true; // jei niekada nepateks į else tai ir išeis
                                      // nedarau kol kas break; kad per anksti neišeitų iš ciklo ir nelabai to reik
                                      // standartinė situacija
                    }
                    else if (table[j][k] != "*" && table[j][k] != "/")
                    {
                        Fixed = false;
                        // splitiname listą:
                        // * viską perrašome skipinant iki kito '*'
                        int ilgis = table[j].Count;
                        // j++; // pasiruošiame pildyti naują row
                        table.Add(new List<string>());
                        bool activate = false; // ignoration
                        for (int s = 0; s < ilgis; s++) // ilgis - 1 nes vienu sumažės
                        {
                            // viską perrašome iš senojo listo išskyrus viską kas eina po rastojo skaičiaus k
                            List<string> senasisRow = table[j]; // buvo j + 1, bet gi j nepasikeitė
                                                                // kol activate nepirdėti į naują listą nieko
                                                                // if (s != k) // šiaip gal labiau viską išskyrus viską iki kito *
                            if (!activate) // šitas aukščiau if s==k nes gi reikia praleisti k
                            {
                                // surašome į naujajį row, bet kaip jį indeksuoti???
                                // table[j + 1].Add(senasisRow[s]);
                                int kiekCia = table.Count - 1;
                                string katoki = senasisRow[s];
                                table[table.Count - 1].Add(senasisRow[s]);
                                // string x = senasisRow[s];
                            }
                            if (s == k)
                            {
                                activate = true;
                            }
                            if (senasisRow[s] == "*" || senasisRow[s] == "/")
                            {
                                //įtraukti reik

                                // lyg prideda anksčiau
                                if (activate)
                                    table[table.Count - 1].Add(senasisRow[s]);
                                activate = false;
                            }

                        }
                        // iš senojo row ištriname k
                        table[j].RemoveAt(k);

                        //lygtais turim dingti iš čia
                        break;

                        // * ir subTextList[k] sename liste
                        // naują listą kišame į table
                    }
                    // ženklo įtraukimas
                    //if (subTextList[j] == "*" || subTextList[j] == "/")
                    //{
                    //}
                }
                //}


                // jei kažkaip praeis visus teisingai tada turėtų aktyvuotis Fixed = true;

                // j reiks kažkaip restartint kad nuo pradžiū žiūrėtų kai pasiekia galą
            }

            return table;
        }

        /// <summary>
        /// Tikslas: maksimalaus indekso radimas (skirtas list lentelei, kad būtų pereita per visus listus t.y. neliktų nesudaugintų dėmenių)
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private int MaxIndex(List<List<string>> table)
        {
            int max = 0;
            int maxIndex = 0;
            for (int index = 0; index < table.Count; index++)
            {
                if (table[index].Count > max)
                {
                    maxIndex = index;
                    max = table[index].Count;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// Tikslas: surasti visus 1 lygio skliaustų vardikl. ir sudauginti reikiamus narius
        /// Rezultatas: nebelieka vardiklių 1 lygio skliaustuose - padarytas progresas atskliaudžiant "/"
        /// </summary>
        /// <param name="text"></param>
        private string CommonDenominatorSimplificationSearchAndFunctionSimplificaiton(string text, ref List<string> denominatorsList)
        {
            // bool complexDivisionExist = true; // darome prielaidą, jog yra sudėtingos divisijos
            bool foundDenominator = true;

            List<List<bool>> boolListas = new List<List<bool>>();

            // denominatorsList = new List<string>();

            int denominatorIndex = 0;

            text = text;

            while (foundDenominator) // while (complexDivisionExist) // kažkur išėjima padarysime iš complex division // man atrodo foundDenominator yra tikrasis išėjimas
            {
                // complexDivisionExist = false; // tada iškart darome prielaidą, kad nėra, kad jeigu tokios nerastų baigtųsi while ciklas
                foundDenominator = false;
                string denominator = "1"; // konkrečios iteracijos denominatorius, bet gal keisis į tą listinį, nežinau, žiūrėsim
                int subStringIndex = 0;
                boolListas.Add(new List<bool>());

                // List<string> denominatorsList = new List<string>();
                for (int i = 0; i < text.Length; i++)
                {

                    boolListas = boolListas;

                    // denominatorsList.Add(""); // pridedame netikrą tuščia denominatorių

                    // boolListas[denominatorsList.Count-1] = new List<bool>();

                    // man atrodo geriausia būtų eiti per paskaidytą ciklą

                    // einam per ciklą ir ieškom visų "/(" subText'e

                    // jei nieko nerandam iki sudėties ar atimties žymim trueFalse[sudėtingasDaliklioNumeris][dėmens numeris] ... lentelėje "true" t.y. dar pradžioje net nežinome koks yra "sudėtingasDaliklis"

                    // radus užžymime nuo "/" iki ")", tą dalį ištriname, ji įkeliame į List, o lentelėje pažymime false

                    // praskipinam visą dalį iki kito pliuso, kad tikrintume kitas dalis, nes ši nebedomina šiuo metu

                    // vėlgi jei kitoje dalyje randame užžymime false ir triname, jei ne true

                    // darome tai kol baigiasi ciklas (kas toliau skaityti už ciklo)

                    text = text;

                    string subText = "";

                    int indexas = i;

                    DivideTextToSubTextForDivisionSimplifier(indexas, text, ref subText); // i nesikeičia

                    subStringIndex++; // reik kažkur skaičiuoti kelintas subText tiriamas, nes tai reikalinga trueFalse lentelėje (iš tiesų nelabai bet why not)

                    string subTextBeforeChanges2 = subText;

                    // čia vykdome ieškojimą "/(", trynimą rastos dalies ir pildome TrueFalse listą ir dar užžymėjimą, jog complexDivisionExist yra tiesa
                    // IDĖJA: kai randa ieško kad sutaptų sekančiame (nebeeina jau per ciklą) [t.y. if (found) vienoks tyrinėjimas, jei false originalusis] ir jei sutampa ištrina patį pirmą
                    //  yr tiesa; tampa liste false ir tada ištriname pirmą
                    // be to nebetiriame subText išto ir skipiname iki kito tiriamojo
                    // 
                    if (!foundDenominator) // jei nerandamas denominatorius, turėtų į boolListą įrašyti TRUE pagal defaultą (kas bus jei ir neras denominatoriaus?
                                           // jei jo išvis neras turėtų užsibaigti funkcija prieš bet kokius veiksmus kad nebūtų perteklinė daugyba iš "1"
                    {
                        // ieškome denominatoriaus (trynimą iškart darome jaučiu, jei darome reik užsižymėti pradžią ir galą, ką ištriname)
                        int indexBalance2 = 0;
                        for (int j = 0; j < subText.Length; j++) // jei neras denominatoriaus tiesiog uzsibaigs ciklas
                        {
                            if (subText[j] == '(')
                            {
                                indexBalance2++;
                            }
                            if (subText[j] == ')') // turi žiūrėt vienu į priekį jeigu teigiamas, jei neigiamas skaičius du į priekį
                            {
                                indexBalance2--;
                            }
                            // viskas ignoruojama skliaustų viduje (ignoruojami ir '/' taip pat)
                            if (indexBalance2 == 0)
                            {
                                if (j + 2 < subText.Length)
                                {
                                    int miniIndexBalance = 0;
                                    int jBeginning = j;
                                    // turi iškirpti "/b" ir perdaryti į "1/b"
                                    if (
                                        (subText[j] == '/') && (subText[j + 1] == '(')
                                        || ((subText[j] == '/') && (subText[j + 1] == '-') && (subText[j + 2] == '(')))
                                    {
                                        foundDenominator = true; // jeigu rado denominatorių, kol kas naujo neiškome
                                        denominator = ""; // nunuliname, nes iš pradžių buvo "1"
                                        j++; // reikia pastumpti kad netikrintų to paties
                                        while (true)
                                        {
                                            // jei i+1 skliausto atsidarymas užsižymime ir įrašome iki skliausto uždarymo bei ženklo už jo
                                            if (subText[j] == '(')
                                            {
                                                miniIndexBalance++;
                                            }
                                            if (subText[j] == ')') // turi žiūrėt vienu į priekį jeigu teigiamas, jei neigiamas skaičius du į priekį
                                            {
                                                miniIndexBalance--;
                                            }
                                            if (miniIndexBalance == 0)
                                            {
                                                if (subText[j] == '/' || subText[j] == '*')
                                                {
                                                    break;
                                                }
                                                // break prieš įrašant paskutinį ženklą
                                                denominator += subText[j];
                                            }
                                            else
                                            {
                                                denominator += subText[j];
                                            }
                                            j++;
                                            if (j == subText.Length)
                                            {
                                                // ended = true;
                                                break;
                                            }
                                        }
                                        //int jEnd = j - 1; // nepaimame paskutinio ženklo, o jei ženklo nėra???
                                        //if (ended)
                                        int jEnd = j;

                                        // vėliau pridėsim denominatorių

                                        denominator = denominator;

                                        subText = subText.Remove(jBeginning, jEnd - jBeginning).Insert(jBeginning, ""); // j beggining įtraukiame, nes tai '/'

                                        j = jBeginning - 1; // reik -1 daryt nes jis iškart pakels
                                                            // nunulinam kad šiukšlių nebūtų
                                                            // j = 0;
                                                            // int ilgis = subText.Length;
                                    }
                                }
                            }
                            if (foundDenominator) // nebereikia ieškoti daugiau denominatorių
                                break;
                        }

                        if (foundDenominator)
                        {
                            // vyksta replace veiksmas
                            // denominatorsList.RemoveAt(denominatorsList.Count - 1); // paskutinį ištriname
                            denominatorsList.Add(denominator);
                            denominatorIndex = denominatorIndex;
                            // denominatorsList.Add(denominator.Remove(0, 1)); // ir įdedame į jo vietą // panaikina "/"
                            boolListas[denominatorIndex].Add(false);
                            // denominatorIndex teoriškai pridedamas
                        }
                        else // šitas defaultas, jeigu nėra denominatoriaus
                        // jei praeina per visą ir neranda dominatoriaus turėtų likti false, o į boolList įrašome true
                        {
                            boolListas[denominatorIndex].Add(true); // nieko naujo nepridedame į denomintorsList, tačiau pridedame į boolListą
                        }
                    }
                    else
                    {
                        if (Regex.Matches(subText, denominator).Count > 0) // jei randamas
                        {
                            // denominatorius be dalybos
                            var regex = new Regex(Regex.Escape("/" + denominator));
                            subText = regex.Replace(subText, "", 1); // tai ir pašalina pirmą denominatorių
                            // užžymime TrueFalseListe = false
                            boolListas[denominatorIndex].Add(false);
                        }
                        else
                        {
                            boolListas[denominatorIndex].Add(true);
                        }
                    }

                    // kur žymime listą prieš randant denominatorių?

                    text = text.Remove(indexas, subTextBeforeChanges2.Length).Insert(indexas, subText); // sukiša pakeistą subText

                    i = subText.Length + indexas; // skipiname iki kito kurį tikrinsime

                    i = i;
                }

                denominatorsList = denominatorsList;

                boolListas = boolListas;

                // pasibaigus ciklui vėl ieškosime "/(" tol kol neberasime (kažkur pažymėti complexDivisionExist)
                // KOMENTARAS: reik suprast kad po ciklo nebelieka pirmo, antro ir t.t. "/(", tad kitame cikle ras kitą,
                // bet to paties neberas nes jis jau ištrintas, tam ir darome true false lentelę
                // ir logiškai kažkada neberas išvis reik prisiminti jog paieška vykdome
                // IKI PIRMO LYGIO SKLIAUSTŲ, kas jų viduje mums nerūpi, nes tą išsiaiškins kiti algoritmai
                if (!foundDenominator)
                {
                    break; // nes neradome denominatoriaus, reiškia visi suprastinti arba panaikinti (šiaip ciklas ir pats išeis šituo atveju, bet ai tegu būna)
                }

                denominatorIndex++;
            }

            denominatorsList = denominatorsList;

            boolListas = boolListas;
            // paskutinį listą ignoruojame
            int ilgis = boolListas.Count - 1;
            boolListas.RemoveAt(ilgis);

            int indexBalance = 0;
            denominatorIndex = 0;

            if (boolListas.Count > 0)
            {
                for (denominatorIndex = 0; denominatorIndex < denominatorsList.Count; denominatorIndex++)
                {
                    int demuo = 0;
                    for (int i = 0; i < text.Length; i++)
                    {
                        boolListas = boolListas;
                        if (text[i] == '(')
                        {
                            indexBalance++;
                        }
                        if (text[i] == ')') // turi žiūrėt vienu į priekį jeigu teigiamas, jei neigiamas skaičius du į priekį
                        {
                            indexBalance--;
                        }
                        // viskas ignoruojama skliaustų viduje (ignoruojami ir '/' taip pat)
                        if (indexBalance == 0)
                        {
                            if (i + 1 < text.Length)
                            {
                                if ((text[i + 1] == '+')
                                    || (text[i + 1] == '-' && text[i] != '*' && text[i] != '/'))
                                {
                                    string denominatorToPut = "";
                                    // vieta kur įmetame pagal boolList
                                    if (boolListas[denominatorIndex][demuo] == true)
                                    {
                                        demuo = demuo;
                                        denominatorToPut = "*" + denominatorsList[denominatorIndex];
                                        text = text.Insert(i + 1, denominatorToPut);
                                    }

                                    i = i + denominatorToPut.Length;

                                    demuo++;
                                    // turime atnaujinti i ir pastumpti šiek tiek į priekį (per vieną)
                                }
                            }
                            else if (i + 1 == text.Length)
                            {
                                string denominatorToPut = "";
                                if (boolListas[denominatorIndex][demuo] == true)
                                {
                                    demuo = demuo;
                                    denominatorToPut = "*" + denominatorsList[denominatorIndex];
                                    text = text.Insert(i + 1, denominatorToPut);
                                    // vieta kur įmetame pagal boolList ir baigiasi ciklas
                                }

                                i = i + denominatorToPut.Length;

                                // nebereikia prideti demens, nes galas
                            }
                        }
                    }
                }
            }
            // jei boolListas yra pirmo lygio bei visi jo elementai yra true, neturėtų būti vykdomas šis veiksmas nes tai reikš, jog nėra ką dauginti (ir gal jei dominatorius = false, bet jis gale bus vis tiek false tai nžn dėl šito)
            // tačiau jei ir nebus tikrinimo mano algoritmas saugus, nes tiesiog daugintų viską iš 1, bet jį apskliaustų ir tai būtų bereikalingas veiksmas
            // išėjus iš ciklo vyksta daugybą kiekvieno nario iš užžymėtų elementų (galbūt sudėti narius į list kad būtų paprasčiau?)
            // nes tuomet tiesiog visus juos sudėtume ir gautume variantą be sudėtingos dalybos (na, o toliau vėl vyktų DivisionSimplifier (prispamintų vienetukų)),
            // po jo atskliaudimas kur daugyba (jei neliks dalybos ties tuo ir užsibaigs ir bus įvykdyta nuskliaustinimas. Po to galėsime ieškoti polinomų ir galiausiai atsakymo
            //

            boolListas.Clear();

            text = text;

            return text;
        }

    }
}
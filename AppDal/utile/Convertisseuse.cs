using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace arc.utile
{
    public class Convertisseuse
    {
        private int mintnum;

        public Convertisseuse(int num)
        {
            mintnum = num;
        }
        public Convertisseuse()
        {
            mintnum = 0;
        }

        public string convertion(string num)
        {
            string res = "";
            string partie1 = "";
            string partie2 = "";
            string partie3 = "";
            string partie4 = "";
            int num_length;
            //identification de la longueur du nombre
            num_length = num.Length;
            if (num_length < 4)
            {
                //nb en centaines
                res = lecture_de_nb_a_3_chiffre(num);
            }
            if (num_length > 3 & num_length < 7)
            {
                partie2 = lecture_de_nb_a_3_chiffre(num.Substring(num.Length - 3, 3));
                partie1 = lecture_de_nb_a_3_chiffre(num.Substring(0, num.Length - 3));
                //si (partie1=un) ne pas écrire
                if (partie1.Trim() == "zéro")
                {
                    res = "";
                }
                else if (partie1.Trim() == "un")
                {
                    res = " mille(s)";
                }
                else
                {
                    res = partie1 + " mille(s)";
                }

                if (partie2.Trim() != "zéro")
                {
                    res = res + " " + partie2;
                }


            }
            if (num_length > 6 & num_length < 10)
            {
                //nb en milions
                //partie3 = lecture_de_nb_a_3_chiffre(num.Substring(num.Length - 3, 3));
                partie2 = convertion(num.Substring(num.Length - 6, 6));
                partie1 = lecture_de_nb_a_3_chiffre(num.Substring(0, num.Length - 6));
                if (partie1.Trim() == "zéro")
                {
                    res = "";
                }
                else if (partie1.Trim() == "un")
                {
                    res = "un million(s) ";
                }
                else
                {
                    res = partie1 + " million(s) ";
                }

                if (partie2 != "zéro")
                {
                    res = res + partie2;
                }

            }
            if (num_length > 9 & num_length < 13)
            {
                partie2 = convertion(num.Substring(num_length - 9, 9));
                partie1 = lecture_de_nb_a_3_chiffre(num.Substring(0, num_length - 9));

                if (partie1.Trim() == "zéro")
                {
                    res = "";
                }
                else if (partie1.Trim() == "un")
                {
                    res = "un milliard(s) ";
                }
                else
                {
                    res = partie1 + " milliard(s) ";
                }

                if (partie2 != "zéro")
                {
                    res = res + partie2;
                }

                //
            }
            return res;
        }

        private string lecture_de_nb_a_1_chiffre(string nb)
        {
            string[] de_zero_a_9 = { "zéro", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf" };
            string res = "";
            int i;

            for (i = 0; i <= 9; i++)
            {
                if ((int.Parse(nb) == i))
                {
                    res = de_zero_a_9.ToArray()[i];
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            return res;
        }
        private string lecture_de_nb_a_2_chiffre(string nb)
        {
            string[] de_1_a_9 = { "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf" };
            string[] de_11_a_19 = { "onze", "douze", "treize", "quatorze", "quinze", "seize", "dix-sept", "dix-huit", "dix-neuf" };
            string[] dizaine_de_10_a_90 = { "dix", "vingt", "trente", "quarante", "cinquante", "soixante", "soixante-dix", "quatre-vingt", "quatre-vingt-dix" };
            char[] les_chiffres;

            les_chiffres = nb.ToCharArray();
            string res = "";
            int i;
            if (les_chiffres[0] == '1')
            {
                if ((int.Parse(les_chiffres[1].ToString()) == 0))
                {
                    res = dizaine_de_10_a_90[0];
                }
                else
                {
                    for (i = 0; i <= 8; i++)
                    {
                        if ((int.Parse(les_chiffres[1].ToString()) == i + 1))
                        {
                            res = de_11_a_19[i];
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
            }
            if ((les_chiffres[0] == '2') || (les_chiffres[0] == '3') || (les_chiffres[0] == '4') || (les_chiffres[0] == '5') || (les_chiffres[0] == '6') || (les_chiffres[0] == '8'))
            {
                //If (CType(les_chiffres(1).ToString, Integer) = 0) Then
                //si le deuxième chiffre est un zéro
                if ((les_chiffres[0] == '2'))
                {
                    if ((int.Parse(les_chiffres[1].ToString()) == 0))
                    {
                        res = dizaine_de_10_a_90[1];
                    }
                    else
                    {
                        for (i = 0; i <= 8; i++)
                        {
                            if ((int.Parse(les_chiffres[1].ToString()) == i + 1))
                            {
                                res = dizaine_de_10_a_90[1];
                                res = res + "-" + de_1_a_9[i];
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }

                if ((les_chiffres[0] == '3'))
                {
                    if ((int.Parse(les_chiffres[1].ToString()) == 0))
                    {

                        res = dizaine_de_10_a_90[2];
                    }
                    else
                    {
                        for (i = 0; i <= 8; i++)
                        {
                            if ((int.Parse(les_chiffres[1].ToString()) == i + 1))
                            {
                                res = dizaine_de_10_a_90[2];
                                res = res + "-" + de_1_a_9[i];
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }
                if ((les_chiffres[0] == '4'))
                {
                    if ((int.Parse(les_chiffres[1].ToString()) == 0))
                    {

                        res = dizaine_de_10_a_90[3];
                    }
                    else
                    {
                        for (i = 0; i <= 8; i++)
                        {
                            if ((int.Parse(les_chiffres[1].ToString()) == i + 1))
                            {
                                res = dizaine_de_10_a_90[3];
                                res = res + "-" + de_1_a_9[i];
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }
                if ((les_chiffres[0] == '5'))
                {
                    if ((int.Parse(les_chiffres[1].ToString()) == 0))
                    {

                        res = dizaine_de_10_a_90[4];
                    }
                    else
                    {
                        for (i = 0; i <= 8; i++)
                        {
                            if ((int.Parse(les_chiffres[1].ToString()) == i + 1))
                            {
                                res = dizaine_de_10_a_90[4];
                                res = res + "-" + de_1_a_9[i];
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }
                if ((les_chiffres[0] == '6'))
                {
                    if ((int.Parse(les_chiffres[1].ToString()) == 0))
                    {

                        res = dizaine_de_10_a_90[5];
                    }
                    else
                    {
                        for (i = 0; i <= 8; i++)
                        {
                            if ((int.Parse(les_chiffres[1].ToString()) == i + 1))
                            {
                                res = dizaine_de_10_a_90[5];
                                res = res + "-" + de_1_a_9[i];
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }
                if ((les_chiffres[0] == '8'))
                {
                    if ((int.Parse(les_chiffres[1].ToString()) == 0))
                    {

                        res = dizaine_de_10_a_90[7];
                    }
                    else
                    {
                        for (i = 0; i <= 8; i++)
                        {
                            if ((int.Parse(les_chiffres[1].ToString()) == i + 1))
                            {
                                res = dizaine_de_10_a_90[7];
                                res = res + "-" + de_1_a_9[i];
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }
            }

            if ((les_chiffres[0] == '7') || (les_chiffres[0] == '9'))
            {
                //si le deuxième chiffre est un zéro
                if ((les_chiffres[0] == '7'))
                {
                    if ((int.Parse(les_chiffres[1].ToString()) == 0))
                    {
                        res = dizaine_de_10_a_90[6];
                    }
                    else
                    {
                        for (i = 0; i <= 8; i++)
                        {
                            if ((int.Parse(les_chiffres[1].ToString()) == i + 1))
                            {
                                res = dizaine_de_10_a_90[5];
                                res = res + "-" + de_11_a_19[i];
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }
                if ((les_chiffres[0] == '9'))
                {
                    if ((int.Parse(les_chiffres[1].ToString()) == 0))
                    {
                        res = dizaine_de_10_a_90[8];
                    }
                    else
                    {
                        for (i = 0; i <= 8; i++)
                        {
                            if ((int.Parse(les_chiffres[1].ToString()) == i + 1))
                            {
                                res = dizaine_de_10_a_90[7];
                                res = res + "-" + de_11_a_19[i];
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }
            }
            return res;
        }
        private string lecture_de_nb_a_3_chiffre(string nb)
        {
            string[] de_2_a_9 = { "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf" };
            const string str_cent = "cent";
            char[] les_chiffres;
            string res = "";
            int i;
            les_chiffres = nb.ToCharArray();


            switch (nb.Length)
            {
                case 1:
                    res = lecture_de_nb_a_1_chiffre(nb);
                    break;
                case 2:
                    res = lecture_de_nb_a_2_chiffre(nb);
                    break;
                case 3:
                    if (les_chiffres[0] == '1')
                    {
                        res = str_cent;
                    }
                    else
                    {
                        for (i = 0; i <= 7; i++)
                        {
                            if (int.Parse(les_chiffres[0].ToString()) == i + 2)
                            {
                                res = de_2_a_9[i] + " " + str_cent;
                            }
                        }
                    }

                    if (les_chiffres[1] == '0')
                    {
                        string partie_intermediaire = "";
                        partie_intermediaire = lecture_de_nb_a_1_chiffre(nb.Substring(2));
                        if (partie_intermediaire != "zéro")
                        {
                            res = res + " " + partie_intermediaire;
                        }
                    }
                    else
                    {
                        res = res + " " + lecture_de_nb_a_2_chiffre(nb.Substring(1));
                    }

                    break;
                default:
                    break;

            }
            return res;
        }
    }
}

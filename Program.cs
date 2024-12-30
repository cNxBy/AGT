using System;
using System.Diagnostics.SymbolStore;
using System.Threading;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AGT
{

    internal class Program
    {
        static void Main(string[] args)
        {
            // Bu dizilerle yapılacak olan işlemler kaydedilir ve gerekli yerlerde kullanma imkanı elde edilir
            // Köşeli parantezin içindeki sayılar '1000' kayıtta tutacağı miktarı belirler
            int[] stok = new int[1000];
            int[] barkod = new int[1000];
            string[] urun = new string[1000];
            string[] satici = new string[1000];
            int[] kredi = new int[1000];
            double[] fiyat = new double[1000];
            double[] musteriTutar = new double[1000];
     
            string[] sepetUrun = new string[1000];
            int[] sepetUrunAdet = new int[1000];
            int[] kod = new int[1000];
            string[] yorumlar = new string[1000];
            int[] teslimGun = new int[1000];
           
            int sepetIndex = 0;

            int yorumlarIndex = 0; // yorumları yazabilmek ve listeleyebilmek için atadığım index
            int pm = 0; // ürünü temsil eder
            int p = 1; // stok miktarı için kullanılır
            double kazanc = 0; //Şirket gelirini tanımlayabilmek için kullanılır

            // Listede Kalıcı Eklenen Ürün
            urun[pm] = "Laptop";
            fiyat[pm] = 15000.50;
            stok[pm] = 10;
            satici[pm] = "ABC Elektronik";
            kredi[pm] = 1;
            barkod[pm] = p++;
            pm++;

            urun[pm] = "Telefon";
            fiyat[pm] = 8000.75;
            stok[pm] = 20;
            satici[pm] = "XYZ Teknoloji";
            kredi[pm] = 10;
            barkod[pm] = p++;
            pm++;

            urun[pm] = "Kulaklık";
            fiyat[pm] = 250.99;
            stok[pm] = 50;
            satici[pm] = "Kulaklık Market";
            kredi[pm] = 0;
            barkod[pm] = p++;
            pm++;

            while (true)
            {
                // İşlem Listesi Gösterilir ve Kullanıcıdan Seçim İstenir
                Console.WriteLine("*********************************************************");
                Console.WriteLine("AGT'ye Hoşgeldiniz Lütfen Yapmak İstediğiniz İşlemi Seçiniz");
                Console.WriteLine("0-Ürün Ekle");
                Console.WriteLine("1-Alışveriş Listesi Görüntüleme");
                Console.WriteLine("2-Alışveriş Yapmak");
                Console.WriteLine("3-Teslimat ");
                Console.WriteLine("4-Şirket Kazancı");
                Console.WriteLine("5-Yorum ve Geri Dönüş");
                Console.WriteLine("6-Yorum ve Geri Dönüş Listesi");
                Console.WriteLine("7-Çıkış");
                Console.WriteLine("*********************************************************");
                string sec = Console.ReadLine();
                //Ürün Ekleme
                if (sec == "0")
                {
                    Console.Clear();
                    Console.Write("Ürün:");
                    urun[pm] = Console.ReadLine();
                    Console.Write("Fiyat:");
                    fiyat[pm] = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Stok Miktarı:");
                    stok[pm] = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Satıcı Firma ya da Şahıs:");
                    satici[pm] = Console.ReadLine();
                    Console.Write("Güvenilirlik Kredisi:");
                    kredi[pm] = Convert.ToInt32(Console.ReadLine());
                    bool urunSorgu = false;
                    for (int i = 0; i < pm; i++)
                    {
                        if (urun[i] == urun[pm] && satici[i] == satici[pm] && fiyat[i] == fiyat[pm])
                        {
                            urunSorgu = true;
                            stok[i] += stok[pm];
                            Console.WriteLine($"{urun[i]} Listede Mevcut Stok Arttırıldı. Yeni Stok {stok[i]}");

                        }

                    }
                    if (urunSorgu == false)
                    {
                        Console.WriteLine("Ürün Listeye Eklendi");
                        barkod[pm] = p++;
                        pm++;
                    }
                }
                //Liste Görüntüleme
                else if (sec == "1")
                {
                    Console.Clear();
                    Console.WriteLine(" Ürün Alışveriş Listesi");
                    for (int i = 0; i < pm; i++)
                    {
                        Console.WriteLine($"Barkod:{barkod[i]} Ürün:{urun[i]}  Fiyat:{fiyat[i]} Stok:{stok[i]}  Firma:{satici[i]}  Güvenilirlik:{kredi[i]}");
                    }
                }
                //Alışveriş Yapma
                else if (sec == "2")
                {
                   
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Menüye dönmek için '0' yazınız");

                        for (int i = 0; i < pm; i++)
                        {
                            Console.WriteLine($"Barkod:{barkod[i]} Ürün:{urun[i]}  Fiyat:{fiyat[i]} Stok:{stok[i]}  Firma:{satici[i]}  Güvenilirlik:{kredi[i]}");
                        }
                        
                        bool urunbul = false;
                        Console.Write("Almak İstediğiniz Ürünün Barkodunu Giriniz: ");
                        int Alurun = int.Parse(Console.ReadLine());
                        if (Alurun == 0)
                        {
                            break;
                        }
                        for (int i = 0; i < pm; i++)
                        {

                            if (barkod[i] == Alurun)
                            {
                                urunbul = true;
                                if (kredi[i] == 0)
                                {
                                    Console.WriteLine("Satıcı Güvenilir Değil");
                                    Console.Read();
                                }
                                if (kredi[i] > 0)
                                {
                                    Console.Write("Kaç Adet Almak İstediğinizi Giriniz: ");
                                    int Aladet = int.Parse(Console.ReadLine());

                                    if (stok[i] >= Aladet)
                                    {

                                        Console.Write("Ulaşmasını istediğiniz En Son Tarihi Giriniz (GG/AA/YYYY formatında ve en az 7 iş günü olmalı): ");
                                        string tarihInput = Console.ReadLine(); // Kullanıcıdan Tarih istiyoruz
                                        DateTime siparisTarihi;
                                        if (DateTime.TryParse(tarihInput, out siparisTarihi))
                                        {
                                            DateTime bugun = DateTime.Now;

                                            int gunSayisi = (siparisTarihi - bugun).Days; //Kullanıcının girdiği tarihle bugünün tarihi arasındaki gün sayısı
                                            if (gunSayisi >= 7)
                                            {
                                                Console.Write("Bütçenizi Giriniz!");
                                                double para = double.Parse(Console.ReadLine());
                                                if (para >= fiyat[i] * Aladet)
                                                {
                                                    Console.Clear();
                                                    //Random Komutu İle  Güvenlik kodu için rastgele sayılar oluşur
                                                    Random random = new Random();
                                                    int guvenlikKodu = random.Next(000000, 999999);
                                                    kod[sepetIndex] = guvenlikKodu;
                                                    Console.WriteLine($"Güvenlik Kodunuzu Kimseyle Paylaşmayınız: ({kod[sepetIndex]})");
                                                  
                                                    stok[i] -= Aladet;
                                                    sepetUrun[sepetIndex] = urun[i];
                                                    sepetUrunAdet[sepetIndex] = Aladet;
                                                    musteriTutar[sepetIndex] = fiyat[i]*Aladet;
                                                    teslimGun[sepetIndex] = gunSayisi;
                                                    satici[sepetIndex] = satici[i];
                                                    sepetIndex++;
                                                    Console.WriteLine($"{Aladet} kadar {urun[i]} Başarıyla Sipariş Edildi. {gunSayisi}  Gün İçinde Teslim Edilecektir. Tutar:{fiyat[i] * Aladet} TL  Kalan Tutar{para - (fiyat[i] * Aladet)} TL ");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Bütçeniz Yeterli Değil!");
                                                }
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Ürünün Size Ulaşmasını İstediğiniz Gün Aralığı En Az 7 Gün Olmalıdır ");
                                                Console.ReadLine();

                                            }
                                        }
                                    }


                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Yeterli Stok Bulunamadı!!!");
                                    }


                                }
                            }

                        }
                        if (!urunbul)
                        {
                            Console.Clear();
                            Console.WriteLine("****Hatalı Barkod Girişi****");
                        }
                        Console.ReadLine ();
                    }
                }

                // Teslimat Gerçekleşti mi
                else if (sec == "3")
                {
                    if (sepetIndex > 0)
                    {

                        Console.Clear();
                        Console.WriteLine("Teslimat Listesi:");
                        for (int i = 0; i < sepetIndex; i++)
                        {
                            Console.WriteLine($"Barkod: {barkod[i]}, Ürün: {sepetUrun[i]}, Adet: {sepetUrunAdet[i]}, Teslim Süresi: {teslimGun[i]} gün, Satıcı: {satici[i]}");
                        }

                        Console.Write("Lütfen teslimat yapmak istediğiniz barkod numarasını giriniz:");
                        int barkodNo = int.Parse(Console.ReadLine());

                        bool teslimatBulundu = false;

                        for (int j = 0; j < sepetIndex; j++)
                        {
                            if (barkodNo == barkod[j]) // Barkod eşleşmesi
                            {
                                teslimatBulundu = true;

                                // Doğru satıcıyı ekrana yazdır
                                Console.WriteLine($"Seçilen Firma: {satici[j]}");

                                // Teslimat kontrolü
                                Console.WriteLine("Teslimatınız ulaştıysa (E), ulaşmadıysa (H) yazınız:");
                                char cevap = Console.ReadKey(true).KeyChar;
                                //Teslimat Ulaştıysa
                                if (char.ToUpper(cevap) == 'E')
                                {
                                   Console.Write("Kodu Giriniz: ");
                                    // Vermiş Olduğumuz Güvenlik kodu istenir
                                    int guvenlikKoduGiris = Convert.ToInt32(Console.ReadLine());
                                    // Eğer Girilen Kod Aynıysa Kabul Edilir Değilse Reddedilir
                                    if (guvenlikKoduGiris == kod[j])
                                    {
                                        Console.WriteLine("***Teslimat Başarılı.***");
                                        // Şirket kazancı hesaplanır
                                        double komisyon = musteriTutar[j]* 0.03;
                                        kazanc += komisyon;

                                        Console.WriteLine($"Bu teslimattan kesilen komisyon: {komisyon:F2} TL");
                                        for (int k = j; k < sepetIndex - 1; k++)
                                        {
                                            barkod[k] = barkod[k + 1];
                                            sepetUrun[k] = sepetUrun[k + 1];
                                            sepetUrunAdet[k] = sepetUrunAdet[k + 1];
                                            teslimGun[k] = teslimGun[k + 1];
                                            satici[k] = satici[k + 1];
                                            kod[k] = kod[k + 1];
                                            kredi[k] = kredi[k + 1];
                                        }
                                        sepetIndex--;
                                        for (int i = 0; i < pm; i++)
                                        {
                                            // Ürünün HAngi Firmaya Ait Olduğunu Sorguluyor Ona Göre Kredisi Artıyor
                                            if (urun[i] == sepetUrun[j] && satici[i] == satici[j])
                                            {
                                                kredi[i]++;
                                                Console.WriteLine($"Güncellenen kredi: {kredi[i]}");
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Hatalı Güvenlik kodu girişi");
                                        break;
                                    }
                                }
                                //Teslimat Ulaşmadıysa
                                else if (char.ToUpper(cevap) == 'H')
                                {
                                    Console.Clear();
                                    Console.WriteLine($"İstediğiniz ürünü ulaştıramadığımız için üzgünüz! Ödemiş Olduğunuz {fiyat[j] * sepetUrunAdet[j]} TL İade Edildi. ");
                                    Console.WriteLine($"{satici[j]} adlı firmanın kredisi düşürülüyor.");
                                    // Satıcı Firmadan Düşüp Düşmediğini Kontrol Eden Döngü
                                    for (int i = 0; i < pm; i++)
                                    {
                                        // Ürünün HAngi Firmaya Ait Olduğunu Sorguluyor Ona Göre Kredisi Düşüyor
                                        if (urun[i] == sepetUrun[j] && satici[i] == satici[j])
                                        {
                                            stok[i] += sepetUrunAdet[j];
                                            kredi[i]--;
                                            Console.WriteLine($"Güncellenen kredi: {kredi[i]}");
                                            break;
                                        }
                                    }
                                    for (int k = j; k < sepetIndex - 1; k++)
                                    {
                                        barkod[k] = barkod[k + 1];
                                        sepetUrun[k] = sepetUrun[k + 1];
                                        sepetUrunAdet[k] = sepetUrunAdet[k + 1];
                                        teslimGun[k] = teslimGun[k + 1];
                                        satici[k] = satici[k + 1];
                                        kod[k] = kod[k + 1];
                                        musteriTutar[k] = musteriTutar[k + 1];
                                    }

                                    sepetIndex--;
                                }
                            }


                        }
                        //Teslim Aşamasında Hatalı Barkod Girişini Tespit Eder
                        if (!teslimatBulundu)
                        {
                            Console.WriteLine("Girilen barkod numarasına ait bir firma bulunamadı.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Kargoda ürün yoktur.");
                    }
                }


                // Şirketin komisyondan kazandığı toplam tutar
                else if (sec == "4")
                {
                    Console.WriteLine($"Toplam Şirket Kazancı: {kazanc:F2} Tl");
                }
                // Yorumların ve Geri dönüşlerin eklendiği kısım
                else if (sec == "5")
                {
                    Console.WriteLine("*********  YORUM ve GERİ DÖNÜŞ  *********");
                    Console.WriteLine("Menüye dönmek için 'Çıkış'yazın");
                    while (true)
                    {

                        Console.Write("_");
                        string yorum = Console.ReadLine();
                        if (yorum.ToLower() == "çıkış")
                        {
                            
                            break; // Döngüden çık
                        }
                        Console.WriteLine("Yorumlar");
                        yorumlar[yorumlarIndex] = yorum;
                        yorumlarIndex++;
                       

                        for (int i = 0; i < yorumlarIndex; i++)

                        {
                            Console.WriteLine($" *{yorumlar[i]}");
                        }
                       
                    }
                }
                // Yorum ve Geri dönüşleri listelendiği kısım
                else if (sec == "6")
                {
                  
                        Console.Clear();
                        Console.WriteLine("YORUM VE GERİ DÖNÜŞLER");
                        for (int i = 0; i < yorumlarIndex; i++) { Console.WriteLine($" *{yorumlar[i]}*"); }
                    
                }
                //Programı kapatmak için
                else if (sec == "7")
                {
                    Console.WriteLine("Çıkış Yapılıyor");
                    break;
                }
            }

        }
       
    }
   
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;

async void Main(string[] args)
{

}
#region ORM Nedir ?
//  ORM Nedir ?
//  Bir kodun içerisinde SQL Cümleciklerinin olması kodu kirletmektedir. Bir kod metinsel ifadelere çok maruz kalıyorsa zaten ister istemez kirlenmektedir.
//  SQL Cümlecikleri olan bir kodda yazılımcının SQL Bilgisinin çok iyi olması gerekir.Tabi bu SQL dili Oracle olabilir , Postgre Olabilir hepsini bilmesi gerekir. Linq Sorgusu 1 tanedir.
//  Örneğin Dbde bir kolonu değiştirdik o zaman gibip kodda yazdığımız o sql değişkenininde değiştirmemiz gerekir yoksa patlar.
//  Örneğin daha önceden MySQL ile  yazılıyordu müşteri dedi şirket politikamız gereği Oracle a geçiyoruz ozaman bütün projedeki SQL kodunu refactoring etmek gerekir.
//  Veritabanı bağımlılığı çok sıkıntı.(bir üstteki örnek.)
//  OOP'den faydalanamayız.

// Bu dezavantajlardan nasıl kurtulabilir ?
// Yazılım ve veritabanı arasındaki bağlantı üzerinden sorgular eşliğinde veri transferini OOP nimetlerinden istifade ederek sağlayabileceği ve böylece
// kodun da, geliştiricinin de SQL'de bağlılığı olmaksızın hızlı ve kolayca operasyonları gerçekleştirebileceği bir yaklaşım ortaya konmuştur. (ORM)
// Object Relational Mapping yani (Nesne İlişkisel Eşleme) şeklinde bir açılıma sahiptir.
// Geliştirilen yazılım içerisinde OOP yapısına uygun olmayan, katı ve kompleks veritabanı sorguları yerine veritabanı objelerinin, bir OOP nesnesi gibi düşünülerek 
// yazılım tarafından kullanılabilmesine olanak sağlayan bir yaklaşımdır.
// Bu yaklaşıma göre veritabanı, tablolar ve veriler yazılım tarafında birer nesneye karşılık gelmektedirler. Böylece tüm veritabanı süreçlerini OOP kavramlarıyla rahatlıkla yönetebilir
// ve kodu SQL'den arındırabiliriz.
// yani ORM denen yapılanma yazılım ve vt ilişkisini OOP nimetlerinden yararlanacak şekilde yapıyor.
// veritabanı ile yazılım arasında OOP ile kod yazmamızı sağlayan bir yaklaşımdır.

// _context.Employees.ToListAsync() -> select * from Employees burada 1. satırı alıyor örneğin gidiyor 1 nesne oluşturuyor ve o nesneyle eşleştiriyor.
// İlişkisel eşleştirme demesinin sebebide budur.
// veritabanı değişikliliğine hazır bir yapı sunar. Bundan dolayı kullanılması gerekmektedir.


#endregion

#region EF CORE

// EF Core, ORM yaklaşımını benimsemiş bir araçtır. 
// Kod içerisinde OOP nimetlerinden istifade ederek SQL sorguları oluşturmamızı sağlamaktadır.
// Kod içerisinde ihtiyaca binaen geliştirilmiş olan tekrarlı SQL sorgucuklarından kurtarmaktadır.
// Code First ve Database First yaklaşımları eşliğinde veritabanı ile yazılım arasındaki kordinasyonu sağlamaktadır.
/* Kod üzerinden Veritabanı ve tablo,
Constraint,
Sequence,
İlişkili sorgular , 
View,
Stored Procedure,
Function,
Temporal Table 
gibi veritabanı nesneleri oluşturmamızı ve kullanmamızı sağlamaktadır.

*/

// Lınq Sorgularını desteklemektedir.


#endregion

#region Database First ve Code First
/* EF CORE veritabanlarıyla iki farklı yaklaşımı baz alarak çalışmaktadır.
 * Code First
 * Database First
 * Yaklaşım ; bir konuyu , olguyu , yapıyı , sorunu çözümü ele alış bir başka deyişle ona bütünsel olarak bakış biçimidir.
 * Haliyle her yaklaşım bir davranışa özel özneldir.
 * EF Core veritabanı çalışmaları için , veritabanının önceden olup olmaması durumlarına göre farklı yaklaşım sunmaktadır. İşte bu durumlara göre farklı yaklaşımlar 
 * eşliğinde çözüm getirilmesi. öznel olmanın bir göstergesidir.
 * Eğer önceden bir veritabanı varsa bu veritabanı kod kısmındada kodlanmalıdır.
 * Lakin hedef veritabanı , bazxen önceden oluşturulmuş bir veritabanı olabileceği gibi bazen de daha yeni oluşturulacak bir veritabanı olabilmektedir.
 * Yaklaşımın temel amacı 2 durumada karşılık subjektif bir yaklaşım sağlamaktır.
 * Misal olarak, uzun zamandır devam eden ve veritabanı doğal olarak mevcut olan bir projeye katıldığınızı varsayalım ve sizlerden bu veritabanı üzerinden EF Core ORM aracı ile belirli işlemler yapmanızı 
 * bekliyorlar Böyle bir durumda projedeki veritabanının var olmasından dolayı büyük ihtimalle Database First yaklaşımını tercih etmeniz gerekecektir.
 * Lakin veritabanı daha inşa edilmemiş bir projey6e katılım gösteriyor olsaydınız bu durumda da ya DATABASE FİRST Yada CODE FİRST yaklaşımlarından birini tercih edebiliriz.
 * Database First :
 * Ef core ile çalışma yapılacak olan veritabnı önceden oluşturulmuş ve içerisine tablolar yerleştirilerek belirli çalışmalar yapılmış ise bu veritabanına kod kısmında modellemek için en doğru yaklaşım 
 * Database First yaklaşımıdır.
 * Database First var olan veritabanını tersine mühendislik ile analiz edip otomatik olarak kod kısmında modelleyen bir yaklaşımdır.
 * Yani şöyle ki hedef veritabanının belirli talimatlar aracılığıyla otomatik olarak kod kısmında OOP nimetleri eşliğinde modellenmektedir.
 * Scaffold talimatı Şu veritabanının kod ortamına taşı bakalım.
 * DbContext : veritabanına karşı gelen sınıf.
 * Database First Avantajları :
 * Hazır veritabanlarını hızlı bir şekilde modelleyebilmemizi sağlar.
 * Veritabınında süreçte olan değişiklikleri de hızlıca koda aktarmamızı sağlar.
 * Dezavantajları :
 * Kod vt tarafından yönetileceği için veritabanı bilgisi istenir.
   Code First :
   Ef Core ile çalışma yapılacak oaln veritabanı önceden oluşturulmamış ise bu veritabanını kod kısmında modellleyerek ardından bu modele uygun veritanını 
   sunucuda oluşturtan (migration) yaklaşımdır. 
   Database First yaklaşımının tam tersi davranış gerektirir.
   Bu yaklaşımda veritabanı önce kodla tasarlanır, sonra veritabanı sunucusuna gönderilerek veritabanı oluşturulur.
   Avatajları ---
   kod üzerinden veritabanını modellememizi sağlar. 
   Veritabanına dokunmaksızın kod üzerinden gerekli düzenlemeleri ve güncellemeleri hızlıca yapabilmemizi sağlar.
   Herhangi bir veritabanı ihtiyacına gerek kalmamaktadır.
   Database First -> Önce veritabanı sonra kod!
   Code First -> Önce kod, sonra veritabanı!
 
 */

#endregion

#region Yapısal Olarak EF Core Aktörleri
/*
 * Bir ORM aracının veritabanını OOP nimetleriyle temsil edebilmesi için veritabanının ,
 * o veritabanı içerisindeki tabloların ve o tablolar içerisindeki kolon ve nesnelerin programatik olarak bir şekilde modellenmesi gerekmektedir.
 * Bu modelleme class'lar üzerinden gerçekleşecektir.
 * EF Core'da veritabanını temsili sınıf DbContext'tir.
 * Ef Core'da veritabanını temsil edecek olan sınıf DbContext olarak nitelendirilmektedir.
 * Bir class'ın adında DbContext geçmesi yeterli değildir.
 * 
 * Bir class'ın veritabanına karşılık gelen DbContext olabilmesi için DbContext sınıfından
 * türemesi gerekmektedir.
 * public class NortwindDbContext : DbContext
 * {
 * 
 * }
 * DbContext sınıfı nelerden sorumludur ?
 * -------- Konfigürasyon --------
 * Veritabanı bağlantısı , model yapılanmaları ve veritabanı yapılanmaları ve veritabanı nesnesi ile tablo nesneleri arasındaki ilişkileri sağlar.
 * -------- Sorgulama -------
 * Sorgulama operasyonlarını yürütür. Kod tarafında gerçekleştirilen sorgulama adımlarını SQL sorgusuna dönüştürür ve veritabanına gönderir.
 * ------- Change Tracking -----
 * Sorgulama neticesinde elde edilen veriler üzerindeki değişiklikleri takip eder.
 * ------- Veri Kalıcılığı ----- 
 * Verilerin kayıt edilmesi , güncellenmesi ve silinmesi operasyonlarını gerçekleştirir.
 * EF Core'da tabloları temsil edecek sınıflar Entity olarak nitelendirilmektedir.
 * Yeryüzündeki herhangi bir olguyu/nesneyi/objeyi modelleyen sınıfa Entity(varlık) denir.
 * Genelde örneğin vt table ismi Orders sa class ismi Order'dır çünkü siparişler tüm tablo halinde Order ise sadece 1 siparişi temsil eder.
 * public DbSet<Orders> Order {get; set;}
 * Tablo Kolonları Ef Core'da bir tabloyu temsil eden sınıfa entity demiştik içindeki propertyler de kolon oluyor.
 * Veriler ise Entity'lerin instancelarına karşılık gelmektedir.
 */
#endregion

#region Database First Yaklaşımı
/*
 * Reverse Engineering (Tersine Mühendislik)
 * Tersine mühendislik , bir sunucusundaki veritabanının iskelesini kod kısmında oluşturma sürecidir.
 * komutları PMC(Packagae Manager Console veya CLI(cmd) ile yapabiliriz)
 * Scaffold talimatı : Veritabanı iskeletinin kod kısmında modellenmesini sağlayan bir talimat.
 * --------- Talimat -----------
 * Scaffold-DbContext 'Connection String'   Microsoft.EntityFrameworkCore.[Provider]
 * Connection String için gooogle a connectionstring yazarsak buluruz.(bütün vtlerin connection stringleri)
 * Provider o dbnin kütüphanesi örneği SqlServerProvider gibi
 * PMC ile veritabanını modelleyebilmek için aşağıdaki kütüpohanelerin projeye yüklenmesi gerekmektedir.
 * Microsoft.EntityFrameworkCore.Tools
 * Database Provider(Örn; Microsoft.EntityFrameworkCore.SqlServer)
 * Providerları bulmak için google'a Database Providers yazarsak.
 * 2 Şey yükleyeceğiz 1 tools 2.ciside provider(SqlServer). Nugetten
 * 
 * Bu işlemleri dotnet cli ile yapmak.
 * dotnet ef dbcontext scaffold 'Connection String' Microsoft.EntityFrameworkCore.[Provider]
 *dotnet cli'da talimatı verebilmek için tools yerine Desing kütüphanesine ihtiyacımız vardır.
 *Varsayılan olarak veritabanındaki tüm tablolar modellenir. Sadece istenilen tabloların modellenebilmesi için aşağıdaki gibi talimatların verilmesi yeterlidir.
 * dotnet ef dbcontext scaffold 'Connection String' Microsoft.EntityFrameworkCore.[Provider] --table Table1, --table Table2
 * Scaffold-DbContext 'Connection String'   Microsoft.EntityFrameworkCore.[Provider] -Tables Table1,Table2
 *PM> Scaffold-DbContext 'server=localhost; User Id=onur; Database=Northwind; Password=xAJa7bhu*D2g; Trusted_Connection=True; TrustServerCertificate=True' Microsoft.EntityFrameworkCore.SqlServer -Tables Personeller
 * DbContext Adını Belirtme
 * Scaffold ile modellenen veritabanı için oluşturulacak context nesnesi adını veritabanından alacaktır. Eğer ki context nesnesinin adını değiştirmek istiyorsanız aşağıdaki gib çalışabilir.z
 * Scaffold-DbContext 'Connection String'   Microsoft.EntityFrameworkCore.[Provider] -Context ContextName
 * dotnet ef dbcontext scaffold 'Connection String' Microsoft.EntityFrameworkCore.[Provider] --context ContextName
 * Path ve NameSpace belirleme önemli
 * Entityler ve DbContext sınıfı default olarak direkt projenin kök dizinine modellenir ve projenin varsaylan namespace'ini kullanırlar. Eğer ki bunlara müdahale etmek istiyorsanız 
 * Scaffold-DbContext 'Connection String'   Microsoft.EntityFrameworkCore.[Provider] -ContextDir Data -OutputDir Models
 * dotnet ef dbcontext scaffold 'Connection String' Microsoft.EntityFrameworkCore.[Provider] --context-dir Data --output-dir Models
 * Pathi belirlemek için örnek kod: Scaffold-DbContext 'server=localhost; User Id=onur; Database=Northwind; Password=xAJa7bhu*D2g; Trusted_Connection=True; TrustServerCertificate=True' Microsoft.EntityFrameworkCore.SqlServer -Tables Personeller -ContextDir Contexts -OutputDir Entities
 * Namespace belirleme
 * Scaffold-DbContext 'Connection String' Microsoft.EntityFrameworkCore.[Provider] -Namespace YourNamespace -ContextNamespace YourNameSpace
 * 
 * Namespace'i belirlemek için örnek kod :  Scaffold-DbContext 'server=localhost; User Id=onur; Database=Northwind; Password=xAJa7bhu*D2g; Trusted_Connection=True; TrustServerCertificate=True' Microsoft.EntityFrameworkCore.SqlServer -Tables Personeller -ContextDir Contexts -OutputDir Entities -Namespace Example.Entities -ContextNamespace Example.Contexts
 * Model Güncellemesi 
 * Veritabanında olan değişiklikleri kod kısmına yansıtabilmek için Scaffold talimatını tekrar vermeniz gerekmektedir lakin talimat neticesinde ilgili sınıfların zaten var 
 * olduğuna dair hata mesajı sizlere yüksek ihtimalle karşılayacaktır.
 * The following file(s) already exist in directory .... Use the Force flag to overwrite these files
 * Böyle bir durumda veritabanı modeline değişiklikleri manuel olarak yansıtabileceğimiz gibi(ki tavsiye etmeyiz.) dosyalar var dahi olsa zorla yeniden en güncel haliyle
 * modellenmesini sağlayabiliriz. Bunun için aşağıdaki gib Force talimatının verilmesi yeterli olacaktır.
 * örnek kod : Scaffold-DbContext 'Connection String' Microsoft.EntityFrameworkCore.[Provider] -Force 
 * bu kodu yazarak daha önce çektiğimiz kodu güncellemiş oluyoruz.
 * Modellerin özelleştirilmesi
 * Database First yaklaşımında veritabnı nesneleri otomatik olarak modellenmekte ve generate edilmektedir. Bazen bu otomatize olan süreçte
 * manuelde olsa  entity'ler de yahut context nesnesinde özelleştirmeler yapmak isteyebiliriz.
 * Ama biliyoruz ki , veritabnında yapılan değişiklikller neticesinde Force komutu eşliğinde tüm değişiklikler kod kısmına sıfırdan yansıtılabilir 
 * ve bu da yapılan değişikliklerin ezilme riskinin söz konusu olduğu anlamına gelir.
 * Bu tarz özelleştirme durumlarında bizzat model sınıflarını kullanmaktansa bunların partial class'ları üzerinde çalışmak en doğrusudur.
 */

#endregion

#region  Veri Kalıcılığı
// Veri Nasıl Eklenir?
// öncelikle context nesnesinden bir instance gerekir.
//ExampleDbContext context = new ExampleDbContext();
//Product product = new Product()
//{
//    Name = "Test",
//    Quantity = 1,
//    UnitCount = 2,
//};
// AddAsync fonksiyonu
//await context.Products.AddAsync(product);
// SaveChaneges nedir ?
// insert , update ve delete sorgularını oluşturup bir transaction eşliğinde veritabanına gönderip execeute eden bir fonksiyondur.
// eğer ki oluşturulan sorgulardan herhangi biri başarısız olursa tüm işlemleri geri alır(rollback)
//await context.SaveChangesAsync();

// EF Core Açısından Bir Verinin Eklenmesi gerektiği nasıl anlaşılıyor?
// Console.WriteLine(context.Entry(product).State);
// Detached -> herhangi bir durumu yok
// Added -> eklenmiş
// Unchanged -> vtye eklendi değişiklik yapılmamış veri
// Deleted -> silinmiş
// Modified -> güncellenmiş

// Birden  Fazla Veri Eklerken Nelere Dikkat Edilmelidir?
// transaction bir maliyettir bundan dolayı olabildiğince verilerimizi tek transaction ile eklemeye çalışmalıyız.
// await context.AddAsync(product);
// await context.AddAsync(product1);
// await context.AddAsync(product2);
// await context.SaveChangesAsync(); tek bir kere yazmak daha mantıklı.
// await context.AddRange(product,product1 ,product2);
// await context.AddRange(productList);

// Eklenen verinin Generate Id'sini elde etme
// await context.AddAsync(product);
// await context.SaveChangesAsync();
// product.Id -> otomatik set edilir
#endregion

#region Veri Güncelleme
//// Bu şekilde günceller. SaveChanges'ı çagırdıgınızda bunun Update olduğunu anlıyor.
//ExampleDbContext context = new ExampleDbContext();
//Product product = await context.Products.FirstOrDefaultAsync(x=> x.Id == 3);
//product.Name = "Test";
//await context.SaveChangesAsync();

//// ChangeTracker Nedir? Kısaca sonra daha detaylı anlatılacak.
//// ChangeTracker context üzerinden gelen nesnelerin takibinden sorumlu mekanizmadır. Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili 
//// işlemler neticesinde update yahut delete sorgularının oluşturacağı anlaşılır.
//// Change tracker gelen veri üzerinde nasıl bir işlem yapıldıysa ona göre bir sql sorgusu oluşturacağını belirliyor.
//// Eğer veri context'den gelmediyse nolur ?
//Product product1 = new Product()
//{
//    Id = 1,
//    Name = "Test",
//};
//context.Products.Update(product1); // bunun için Update fonksiyonu vardır.Update fonksiyonunu kullanabilmek için kesinlikle Id değeri verilmelidir.
//await context.SaveChangesAsync(); // bunu yine çağırman gerekir.

// EntityState Nedir?
// Bir Entity insantce'ının durumunu ifade eden bir referansdır
//context.Entry(product).State = EntityState.Modified;

//// EF Core Açısından bir verinin güncellenmesi gerektiği nasıl anlaşılıyor?
//Product product2 = await context.Products.FirstOrDefaultAsync(x => x.Id == 3);
//Console.WriteLine(context.Entry(product2).State); // Unchanged
//product2.Name = "Onur";

//Console.WriteLine(context.Entry(product2).State); // Modified
//await context.SaveChangesAsync(); // bunu yine çağırman gerekir.
//Console.WriteLine(context.Entry(product2).State); // Unchanged

//// Birden Fazla Veri Güncellenirken Nelere Dikkat Edilmelidir?
//var urunler = await context.Products.ToListAsync();
//foreach (var item in urunler)
//{
//    item.Name += "*";
//}
//await context.SaveChangesAsync(); // bunun 1 kere çağrılması iyi bir şey.


#endregion

#region Veri Nasıl Silinir Veri Silme Detayları
//ExampleDbContext context = new ExampleDbContext();
//Product urun = await context.Products.FirstOrDefaultAsync(x => x.Id == 5);
//context.Products.Remove(urun);
//context.SaveChanges();

// contextden gelmeyen veriler takip edilemeyen veriler nasıl siliniyor?
//Product p = new Product()
//{
//    Id = 5,
//};
//context.Products.Remove(p);
//context.SaveChanges();

//Entity State ile veri silme işlemi 

//context.Entry(p).State = EntityState.Deleted;
//await context.SaveChangesAsync();

// SaveChanges ı verimli kullanmak önemlidir. 1 kere kullanmak iyidir.

// RemoveRange
//var urunler = await context.Products.Where(x=> x.Id > 5).ToListAsync();
//context.RemoveRange(urunler);
//context.SaveChanges();
#endregion

#region Temel Düzeyde Sorgulama Yapılanmaları 
// EF Core da sorgulama yapabilmek için önce bir context nesnesi lazım sonra Method Syntax ve Query syntax olarak 2 yöntem kullanabiliriz.

// IQueryable ve IEnumarable nedir ?
// sorgu oluşturma kısmı IQueryabledır
//IQueryable Sorguya karşılık gelir. EF Core üzrinden yapılmış olan sorgunun execute edilmemiş halini ifade eder.
// IEnumarable 
// Sorgunun çalıştırılıp/execute edilip verilerin in memorye yüklenmiş halini ifade eder.
// ToListAsync fonksiyonu sorguyu execute etmek için kullandığımız bir fonksiyon
// sorguyu Execute edebilmek için ToListAsync den başka 1 yöntem daha var.
// Foreach ile Deferred Execution(Ertelenmiş Çalışma) yapabiliriz.
// foreach(Urun urun in urunler) bu kısımda execute edilir.
// {
//   Console.WriteLine(urun.UrunAdi);
// }

// int urunId = 5;
// var urunler = from urun in context.Urunler
// where urun.Id > urunId 
// select urun;
// urunId = 200; // burada üstteki sorguda 200 e göre sorgulama yapar.
// bir üstteki sorguda IQueryable da hala sorgu execute edilmedi inşa edilmedi. Execute edildiği anda dış parametrelerin son hali ne ise o alınır
// foreach(Urun urun in urunler) bu kısımda execute edilir.
// {
//   Console.WriteLine(urun.UrunAdi);
// }

// Buradaki mantığı anlamak için Deferred Execution mantığı
// IQueryable çalışmalarında ilgili kod yazıldığı noktada tetiklenmez/ çalıştırılmaz. yani ilgili kod
// yazılıdığı noktada sorguyu generate etmez! Nerede eder ? Çalıştırıldığı/execute edildiği noktada tetiklenir. İşte bu duruma ertelenmiş çalışma denir.
#endregion


#region Çoğul Veri Getiren Sorgulama Fonksiyonları
// ToListAsync
// Üretilen sorguya execute ettirmemizi sağlayan fonksiyondur.
// Iquerable dan IEnumarable' a geçiş yapmamızı sağlıyor.
// ıEnumarable olarak devam etmemizi sağlar.
// OrderBy 
// sorgu üzerinde sıralama yapmamızı sağlar (Ascending)
// ThenBy
// OrderBy üzerinde yapılan sıralama işlemini farklı kolonlarada uygulamamızı sağlayan bir fonksiyondur.
// OrderByDescending
// OrderBy'ın tam tersi Descending olarak sıralar
// ThenByDescending
// ThenBy ın tam tersi descending sıralar.
#endregion

#region Tekil Veri Getiren Sorgulama Fonksiyonları
// SingleAsync : eğer ki , sorgu neticesinde birden fazla veri geliyorsa  ya da hiç gelmiyorsa exception fırlatır.

//SingleOrDefaultASync :birden fazla veri gelirse exception fırlatır. gelmiyorsa null değeri atar.
// await context.Products.SingleAsync( u => u.Id == 55);
// Bir sorgu yazdığımızda sadece 1 tane verinin gelmesini istiyorsak Single yada SingleOrDefault fonksiyonları kullanılabilir.

//FirstAsync
// sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa hata fırlatır.

//FirstOrDefaultAsync
// sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa null değeri atar.
// await context.Products.FirstAsync(x=> x.Id == 55);
//SingleAsync , SingleOrDefaultASync , FirstAsync ,FirstOrDefaultAsync Karşılaştırması
//Single ile first arasındakki fark ne kadar veri gelirse gelsin first için önemli değil single için 1  den fazla veride hata fırlatır.
// FindAsync
// primary key kolonuna hızlı bir şekilde özgü arama yapmak için
// context.Products.FindAsync(55);
// Composite primary key durumu
// context.Products.FindAsync(2,5);

//FindAsync ile SingleAsync , SingleOrDefaultASync , FirstAsync ,FirstOrDefaultAsync Karşılaştırması
// findAsync önce memorye bakar o istenen veri var mı diye yoksa veritabanına gider digerleri hep gider.
// findAsync kayıt bulamazsa null döndürür

// LastAsync ve LastOrDefaultAsync
// sorgudan gelen veirilerden sonuncuyu alır. Order By ile kullanmak zorunludur.

#endregion


#region Diğer sorgulama fonksiyonları
// CountAsync
// Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak (int) bizlere bildiren fonksiyondur.

// context.Products.ToListAsync().Count(); bu bir yöntemdir ancak maliyetli bir yöntemdir. bu şekilde önce memorye getiriyoruz sonra işlemi yapıyoruz.
// var urunler = await context.Products.CountAsync(); bu daha iyi bör yöntemdir.

// LongCountAsync
// eğer count karşılamıyorsa dönen int çok çok büyük bir sayıysa o zaman LongCountAsync kullanılmalıdır.
// ek olarak ikisinede where şartı koyabilirizsin metodun içine
// var urunler = await context.Products.CountAsync(u => u.Id > 10); gibi.

// AnyAsync

// örneğin bir sorgu sonucunda veri geliyor mu gelmiyor mu sql deki exist sorgusunun karşılığıdır.
// bool türünde döner
// var urunler = await context.Urunler.AnyAsync();
// var urunler = await context.Urunler.Where(x => x.UrunAdi.Containst("1")).AnyAsync();
// x => x.UrunAdi.Containst("1") bu şartı AnyAsync() metodunnu içinede yazılabilir. Performans açısından bir fark yoktur.

// MaxAsync

// var fiyat = await context.Urunler.MaxAsync(x => x.Fiyat);
// sadece maximum fiyatı getirir.
// MinAsync
// var fiyat = await context.Urunler.MinAsync(x => x.Fiyat);
// sadece minimum fiyatı getirir.

// Distinct
// Sorguda mükerrer kayıtlar varsa bunları tekilleştiren bir işleve sahip fonksiyondur.
// var urunler = context.Products.Distinct().ToListAsync(); // ToListAsync ypaman gerekir çünkü Distinct IQueryable döner.

// AllAsync
// Bir sorgu neticesinde gelen verilerin , verilen şarta uyup uymadığını kontrol eder eğer tüm veriler uyuyorsa true döner eğer 1 tanesi bile uymuyorsa false döner.
// var m = await context.Urunler.AllAsync(u => u.Fiyat > 5000);

// SumAsync
// Toplam fonksiyonudur.
//  var fiyatToplam = await context.Urunler.SumAsync(u => u.Fiyat);

// AverageAsync
// Vermiş olduğumuz sayısal propertynin aritmatik ortalamasını alır.
//  var fiyatToplam = await context.Urunler.AverageAsync(u => u.Fiyat);

//ContainsAsync
// like sorgusuna karşılık gelir. '%.%' içinde şeklinde bir like sorgusu
// var urunler = await context.Products.Where(u => u.UrunAdi.Contains("a")).ToListAsync();

// StartWith
// Like sorgusu oluşturmayı sağlar ve başlayanlara getirmeyi sağlar.
// var urunler = await context.Products.Where(u => u.UrunAdi.StartsWith("a")).ToListAsync();


// EndsWith
// Like sorgusu oluşturmayı sağlar ve bitenleri getirmeyi sağlar.
// var urunler = await context.Products.Where(u => u.UrunAdi.EndsWith("a")).ToListAsync();



#endregion

#region Diğer fonksiyonlar 2
// En Temel Basit Bir Sorgulama Nasıl Yapılır?

// Sorguyu Execute Etmek İçin Ne Yapmamız Gerekmektedir?

// IQueryable ve IEnumarable Nedir ? Basit Olarak

// Çoğul Veri Getiren Sorgulama Fonksiyonları 

// Tekil Veri Getiren Sorgulama Fonksiyonları 

// Diğer Sorgulama Fonksiyonları 


// Diğer Sorgu Sonucu Dönüşüm Fonksiyonları 
// Bu fonksiyonlar ile sorgu neticesinde elde edilen verileri istediğimiz doğrultusunda farklı türlerde projeksiyon edebiliyoruz

// ToDictionaryAsync
// var urunler = context.Urunler.ToDictionaryAsync(u=> u.UrunAdi (key), u => u.Fiyat (value));
// ToList ile aynı amaca hizmet etmektedir. Yani , oluşturulan sorguyu execute edip neticesini alırlar.
// ToList : Gelen sorgu neticesini entity türünde bir koleksiyona(List<TEntity>) dönüştürmekteyken,
// ToDictionary ise : Gelen sorgu neticesini Dictionary türünden bir koleksiyona dönüştürecektir.

// ToArrayAsync
// Oluşturulan sorguyu dizi olarak elde eder.
// ToList ile muadil amaca hizmet eder. Yani sorguyu execute eder lakin gelen sonucu entity dizisi olarak elde eder.

// Select
// var urunler = await context.Urunler.ToListAsync();
// Select fonksiyonunun işlevsel olarak birden fazla davranışı söz konusudur.
// Select fonksiyonunun generate edilecek sorgunun çekilecek kolonlarını ayarlamamızı sağlamaktadır.
// performanslı bir sorgu istiyorsan IQueryable da kullanman gerekir. seçilen değerler dışında diğerleri null değer atacaktır.
// var urunler = await context.Products.Select(x => new {x.Id , x.Fiyat }).ToListAsync(); Bu şekilde anonim de çalışılabilir.
// 
// SelectMany
// Select ile aynı amaca hixmet eder. Lakin , ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip projeksiyon 
// etmemizi sağlar.
// parcalar bir koleksiyon olsun bu durumda select ilişkisel tablolarda getiremiyor
// var urunler = await context.Products.Include(x=> x.Parcalar).SelectMany(x => x.Parcalar , (u,p) => new {
//  u.Id,
// u.Fiyat,
// p.ParcaAdi
// }).ToListAsync(); 

#endregion

#region Ekstradan Foreach fonksiyonu ile pratik iterasyon

// GroupBy
// gruplama yapmamızı sağlayan fonksiyondur.
// istatiksel veriler için genelde kullanırız.
// context.Products.GroupBy(u => u.Fiyat).Select( group => new
// { Count = group.Count() ,
//  Fiyat = group.Key
// }).ToListAsync();

// Foreach fonksiyonu
// Bir sorgulama fonksiyonu değildir.
// Sorgulama neticesinde elde edilen koleksiyonel veriler üzerinde iterasyonel olarak dönmemizi ve teker teker verileri elde edip işlemler yapabilmemizi sağlayan bir fonksiyondur.
// foreach döngüsünün metot hailidir.

// foreach (var item in datas)
//{

//}

// datas.ForEach(x =>
//{

//});



#endregion

#region ChangeTracker Propertysi

//ChangeTracker Neydi ?
//Context nesnesi üzerinden gelen tüm nesneler otomatik olarak bir takip mekanizması tarafından izlenirler.
//İşte bu takip mekanizmasına ChangeTracker denir. Change Tracker ile nesneler üzerindeki değişiklikler/işlemler 
// takip edilerek netice itibariyle bu işlemlerin fıtratına uygun sql sorgucukları generate edilir. İşte bu işleme de Change Tracking denir.

// Takip edilen nesnelere erişebilmemizi sağlayan ve gerektiği takdirde işlemler gerçekleştirmemizi sağlayan bir properytdir.

// var urunler = await context.Products.ToListAsync();
// var datas = context.ChangeTracker.Entries(); // changetracker da takip edilen tüm veriler gelir. Unchanged biçimde
//örneğin 
// urunler[6].Fiyat = 123;
// context.Remove(urunler[7]);
// urunler[8].UrunAdi = "asdadsddasd";
// var datas = context.ChangeTracker.Entries(); // changetracker da takip edilen tüm veriler gelir. 
// eğer  bu şekile yaparsak 2 tanesi modified birisi deleted olur.
// Console.WriteLine();

//DetectChanges Metodu
// EF Core , context nesnesi tarafından izlenene tüm nesnelerdeki değişiklikleri Change Tracker sayesinde takip edebilmekte ve nesnelerde olan verisel değişiklikler yakalanarak bunların anlık
// görüntüleri(snapshot)'ını oluşturabilir.
// Yapılan değişikliklerin veritabanına gönderilmeden önce algılandığından emin olmak gerekir. SaveChanges fonksiyonu çağrıldığı anda nesneler EF Core tarafından otomatik kontrol edilirler.
// Ancak, yapılan operasyonlarda güncel tracking verilerinden emin olabilmek için değişikliklerin algılanmasını opsiyonelolarak gerçekleştirmek isteyebliriz.
// İşte bunun için DetectChanges fonksiyonu kullanılabilir ve her ne kadar EF Core değişiklikleri otomatik algılıyor olsa da siz yine de iradenizle kontrole zorlayabilirsiniz.
//SaveChanges otomatik olarak zaten DetectChanges'ı çağırır.

// var urun = await context.Products.FirstOrDefaultAsync(x=> x.Id == 3);
// urun.Fiyat = 123;
// context.ChangeTracker.DetectChanges();
// await context.SaveChangesAsync();

// AutoDetectChangesEnabled Property'si
// İlgili metotolar(SaveChanges , Entries) tarafından DetectChanges metodunun otomatik olarak tetiklenmesinin konfigürasyonunu yapmamızı sağlayan propertydir.
// SaveChaneges fonksiyonu tetiklendiğinde DetectChanges metodunu içerisinde default olarak çağırmaktadır. Bu durumda 
// DetectChanges fonksiyonunun kullanımını irademizle yönetmek ve maliyet/performans optimizasyonu yapmak istediğimiz durumlarda AutoDetectChangesEnabled özelliğini kapatabiliriz.

// Entries Metodu
// contexteki Entry metodunun koleksiyonel versiyonudur.
// Change Tracker mekanizması tarafından izlenen her entitiy nesnesinin bilgisini EntityEntry türünden elde etmemizi sağlar ve belirli işlemler yapabilmemize olanak tanır.
// Entries metodu çalışmadan önce DetectChanges metodunu tetikler. Bu durum da tıpkı SaveChanges'da olduğu gibi bir maliyettir.
// Buradaki maliyetten kaçınmak için AutoDetectChangeEnabled özelliğine false değeri verilebilir.
// var urunler = await context.Urunler.ToListAsync();
// urunler.FirstOrDefault(u => u.Id==7).Fiyat = 123;
// context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 8));
// urunler.FirstOrDefautl(u => u.Id == 6).UrunAdi ="sdfsfsf";
// context.ChangeTracker.Entries().ToList().ForEach(e => {
// if(e.State == EntityState.Unchanged)
// {

// }
// if(e.State == EntityState.Unchanged)
// {

// }
// })

// await context.SaveChangesAsync();
// await context.SaveChangesAsync(true); bu ikiside aynı anlama gelir ve track edilen veriler silinsin demektir yani artık takip edilmesin. Bu metot sonrasında track edilen verilerin
// takibi kesilir. Yeni değişikliklerin takip edilmesini bekler. Böyle bir durumda beklenmeyen bir durum/ olası bir hata söz konusu olursa eğer EF Core takip ettiği nesneleri bırakacağı için
// bir düzeltme mevzu bahis olmayacaktır.
// AcceptAllChanges Metodu
// Haliyle bu durumda SaveChanges(false)(başarılı olsada olmasada track etmeye devam eder) ve AcceptAllChanges metotları girecektir.
// SaveChanges(false), EF Core'a gerekli veritabanı komutlarını yürütmesini söyler ancak gerektiğinde yeniden oynatılabilmesi için değişiklikleri beklemeye/nesneleri takip etmeye
// devam eder. Taa ki AcceptAllChanges metodunu idaremizle çağırana kadar

// SaveChanges(false) ile işlemin başarılı olduğundan emin olursanız AcceptAllChanges metodu ile nesnelerden takip kesebilirsiniz.

// HasChanges Metodu
// Takip edilen nesneler arasından değişiklik yapılanların olup olmadığının bilgisini verir.
// Arkaplanda DetectChanges metodunu tetikler.
// var result = context.ChangeTracker().HasChanges();

// Entity States
//Entity nesnelerinin durumlarını ifade eder.
// Detached
// Nesnenin change tracker mekanizması tarafından takip edilmeddiğini ifade eder.
// Urun urun = new();
// Console.WriteLine(context.Entry(urun).State); // detached
// Added
// Veritabanına eklenecek nesneyi ifade eder.Added henüz veritabanına işlenmeyen veriyi ifade eder. SaveChanges foknsiyonu çağrıldığında sorgusu oluşturulacağı anlamına gelir.
// Urun urun = new()  {Fiyat = 123 , UrunAdi ="Ürün 1001"};
// await context.Urunler.AddAsync(urun);
// Console.WriteLine(context.Entry(urun).State); // Added

// Unchanged
// Veritabanından sorgulandığından beri veritabanında herhangi bir değişiklik yapılmadığını ifade eder. Sorgu neticesinde elde edilen tüm nesnelerin ilk değeri Unchanged'dir.

// Modified
// Nesne üzerinde değişiklik/güncelleme yapıldığını ifade eder. SaveChanges fonksiyonu çağrıldığında update sorgusu oluşturulacağı anlamına gelir.
// var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
// urun.Fiyat =500;

// Deleted
// Nensenin silindiğini ifade eder. SaveChanes fonksiyonu çağrıldığında delete sorgusu oluşturulacağı anlamına gelir.

// Context nesnesi üzerinden Change Tracker
// context.Entry bu şekilde tekil bir nesne üzerinden gidebilirsin ya da
// context.ChangeTracker diyerek çoğul nesneler üzerinden de işleme devam edebilirsin.
// OriginalValues Property'si
// var fiyat = context.Entry(urun).OriginalValues<int>(nameof(Urun.Fiyat)); fiyat bilgisinin orijinal değerini verir.

// Current vALUE İLE DE ŞUANKİ DEĞERini getirebilirsin.
// var fiyat = context.Entry(urun).CurrentValues.GetValue<int>(nameof(Urun.Fiyat)); fiyat bilgisinin orijinal değerini verir.

// GetDatabaseValues Metodu
// Database deki değerini getirir.
// var urun = context.Entry(urun).GetDatabaseValuesAsync();

//Change Tracker'ın Interceptor olarak kullanılması 
// altta görüldüğü üzere database e giderken araya girilebilir (saveChanges metodu)
//public class ExampleDbContext : DbContext
//{
//    public DbSet<Product> Products { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
//    public DbSet<Customer> Customers { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // bu fonksiyon context nesnemiz ile ilgili temek konfigürasyonları yapmamızı sağlar. Bir alt regionda teorik bilgiler bulunmaktadır.
//    {
//        // burada Database daha önce yoksa sizin yazdığınız şekilde bir database(ECommerceDbContext) oluşturur.
//        //Provider yapılandırılması
//        // ConnectionString yapılandırılması
//        // Lazy Loading
//        // vb.
//        optionsBuilder.UseSqlServer("server=localhost; User Id=onur; Database=ECommerceDbContext; Password=xAJa7bhu*D2g; Trusted_Connection=True; TrustServerCertificate=True");

//    }

//    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
//    {
//        var entries = ChangeTracker.Entries();
//        foreach (var item in entries)
//        {
//            if(item.State ==EntityState.Added)
//            {

//            }
//        }
//        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
//    }
//}



#endregion


#region AsNoTracking gibi yapılanmalar
//Bu fonksiyonlar ChangeTracker mekanizasının davranışlarını yönetmemizi sağlayan fonksiyonlardır. Context üzerinden gelen tüm datalar Change Tracker mekanizması
//tarafından takip edilmektedir.
// Change Tracker, takip ettiği nesnelerin sayısıyla doğru orantılı olacak şekilde bir maliyete sahiptir. O yüzden üzerinde işlem yapılmayacak verilerin takip edilmesi 
// bizlere lüzumsuz yere bir mailyiet ortaya çıkaracaktır.

// AsNoTracking Metodu 
//AsNoTracking metodu , context üzerinden sorgu neticesinde gelecek olan verilerin Change Tracker tarafından takip edilmesini engeller.
// AsNoTracking metodu ile Change Tracker'ın ihtiyaç olmayan verilerdeki maliyetini törpülemiş oluruz.
// AsNoTracking fonksiyonu ile yapılan sorgulamalarda,verileri elde edebilir , bu verileri istenilen noktalarda kullanabilir üzerinde herhangi bir değişiklik/update
// işlemi yapamayız.
//var kullanicilar = await _context.Kullanicilar.AsNoTracking().ToListAsync(); bunu yaparsak eğer bu tabloda yapılan değişiklikler veritabanına yansımaz
// ama maliyetten kurtulmuş oluruz.
// örneğin kullanicilar[0].Adi ="Onur";
// await context.SaveChangesAsync(); bu değişiklik vtye yansımaz takip edilmediği için.
//amaa eğer ki 
// context.Update(kullanici); bu vtye yansır çünkü burada manuel bir değişiklik yapılıyor.
// remove da çalışır. aslında Update yapmayacaksak ChangeTracker mekanizması takibi bırakılır (AsNoTracking() ile)

// AsNoTrackingWithIdentityResolution
// Change Tracker mekanizması sayesinde yinelenen datalar aynı instanceları kullanırlar.
// AsNoTracking metodi ile yapılan sorgularda yinelenen datalar farklı instancelarda karşılanırlar. İlişkisel verilerde maliyetli olur AsNoTracking
// Böyle bir durumda hem takip mekanizmasının maliyetini ortadan kaldırmak hemde yinelenen dataları tek bir instance üzerinden karşılamak için AsNoTrackingWithIdentityResolution
// fonksiyonunu kullanabiliriz.
// AsNoTrackingWitthIdentityResolution fonksiyonu AsNoTracking foknksiyonuna nazaran görece yavaştır/maliyetlidir lakin CT'a nazaran dha performanslı ve az maliyetlidir.

//AsTracking
// change trackerın default halinin göreceli hali.

// UseQueryTrackingBehavior
// EF Core seviyesinde/uygulama seviyesinde ilgili contexten gelen verilerin üzerinde CT mekanizmasının davranışı temel seviyede belirlememizi sağlayan fonksiyondur. Yani konfigürasyon fonksiyonudur.
// context içindeki OnConfiguring fonksiyonu içine 
// optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

#endregion

#region İlişkisel Yapılar | Aşina Olunması Gereken Temel Kavramlar
// Principal Entity(Asıl Entity)
// Kendi başına var olabilen tabloyu modelleyen entity'e denir.
// Departmanlar tablosunu modelleyen 'Departman enittysidir.'

// Dependent Entity(Bağımlı Entity)
// Kendi başına var olmayan, bir başka ytabloya bağımlı(ilişkisel olarak bağımlı) olan tabloyu modelleyen entity'e denir.
// Calışanlar tablosunu modelleyen 'Calisan' entitysidir.

// Foreign Key 
// Principal Entity ile Dependent Entity arasındaki ilişkiyi sağlayan key'dir.

// Principal Key
// Principal entity deki Idnin kendisidir. Principal Entity in kimliği kolonudur.

// Navigation Property Nedir?
// İlişkisel tablolar arasındaki fiziksel erişimi entity classları üzerinden sağlayan properytidir.
// public Departman Departman {get; set;} ikiside navigation propertyi
// public ICollection<Calisan> Calisanlar {get; set;}
// bir properynin navigation property olabilmesi için kesinlikle entity türünden olması gerekiyor.
// Navigation property'ler entitylerdeki tanımlarına göre n'e n yahut 1'e n şeklinde ilişki türlerini ifade etmektedirler.
// Sonraki derslerimizde ilişkisel yapıları tam teferruatlı pratikte incelerken navigation property'lerin bu özelliklerinden stifade ettiğimizi göreceksiniz.
// İLİŞKİ TÜRLERİ
// One to One
// Birebir ilişkidir. Karı-Koca ilişkisi her kadının 1 kocası her erkeğin bir karısı olabilir.

// One to Many
// Bire cok ilişki. Çalışanların 1 departmanı olabilir. departmanların 1 den fazla çalışanı olabilir.
// Anne ve çocukları arasındaki ilişki

// Many to Many
// Çalışanlar ile projeler arasındaki ilişki 
// kardeşler arasındaki ilişki


// Entity Framework Core'da İlişki Yapılandırma Yöntemleri

// Default Conventions
// Varsayılan Entity kurallarını kullanarak yapılan ilişki yapılandırma yöntemleridir.
// Navigation properytylerini kullanarak ilişki şablonlarını çıkarmaktadır.

// Data Annotations Attributes
// Entitynin niteliklerine göre ince ayarlar yapmamızı sağlayan attributelerdir.

// Fluent API
//Entity modellerindeki ilişkileri yapılandırırken daha detaylı çalışmamızı sağlayan yöntemdir.

// HasOne
// İlgili entity'nin ilişkisel entitiy'ye bire bir ya da bire çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.

// HasMany
// İlgili entity'nin ilişkisel entitiy'ye çoka bir ya da çoka çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.

// WithOne
// HasOne ya da HasMany'den sonra bire bir ya da çoka bir olacak şekilde ilişki yapılandırmasını tamamlayan metottur.

// WithMany
// HasOne ya da HasMany'den sonra bire çok ya da çoka çok
// ,olacak şekilde ilişki yapılandırmasını tamamlayan metottur.

#endregion

#region Tüm Detaylarıyla Birebir İlişki Yapılanması
// Default Convention
// Navigation propertyler tanımlanması gerekir.
// One to One ilişki türünde , dependent entity'nin hangisi olduğunu default olarak belirleyebilmek kolay değildir.
// bu durumda fiziksel foreign keye karşılık gelecek bir kolon tanımlayarak bu durum çözebiliriz.
// böylece foreign keye karşılık gelen luzümsüz bir kolon oluşturmuş oluyoruz.
// class Calisan 
// {
//      public int Id {get; set;}
//      public string Adi {get; set;}
//      public CalisanAdresi CalisanAdresi {get; set;}
// }

// class CalisanAdresi
// {
//      public int Id {get; set;}
//      public int CalisanId {get; set;} EF Core bunu Foreign Key olarak anlayabilir.
//      public string Adi {get; set;}
//      public Calisan Calisan {get; set;}

// }

// yukarıdaki gibi bir durumda CalisanAdresi bağımlı Calisan tablosu ise Principle table dır.
// EF Core un bu durumu anlaması için CalisanAdresi tablosuna CalisanId eklenmesi gerekir. 


// Data Annotations 
// Navigation Propertyler tanımlanmalıdır.
// Foreign kolonun ismi default conventionın dışında bir kolon olacaksa eğer ForeignKey attribute ile bunu bildirebiliriz.
// Foreign Key kolonu oluşturulmak zorunda değildir.
// 1'e 1 ilişkide ekstradan foreign key kolonuna ihtiyaç olmayacağından dolayı dependent entity'deki id kolonunun hem foreign key hem de primary key olarak kullanmayı 
// tercih edioyoruz ve bu duruma özen gösterilir diyoruz.
// class Calisan 
// {
//      public int Id {get; set;}
//      public string Adi {get; set;}
//      public CalisanAdresi CalisanAdresi {get; set;}
// }

// class CalisanAdresi
// {
//      public int Id {get; set;}
//      [ForeignKey("Calisan")] Calisan bu Calisan ismit navigation propertyin değişken ismi daha güzeli [ForeignKey(nameof(Calisan)]
//      public int CalisanId {get; set;} EF Core bunu Foreign Key olarak anlayabilir.
//      public string Adi {get; set;}
//      public Calisan Calisan {get; set;}
// }
// Bunun birebir ilişki olduğunun garantisini index veriyor migrationa oluşturduğumuzda unique özelliği true oluyor.

// class Calisan 
// {
//      public int Id {get; set;}
//      public string Adi {get; set;}
//      public CalisanAdresi CalisanAdresi {get; set;}
// }

// class CalisanAdresi
// {
//      [Key , ForeignKey("Calisan")] Bu şekilde yapılması.
//      public int Id {get; set;}
//      public string Adi {get; set;}
//      public Calisan Calisan {get; set;}
// }

// Fluent API
// Navigation propertyler tanımlanmalı.
// Entityler arasındaki ilişki context sınıfı içinde onModelCreating fonksiyonu içinde tanımlanmalıdır.
// class Calisan 
// {
//      public int Id {get; set;}
//      public string Adi {get; set;}
//      public CalisanAdresi CalisanAdresi {get; set;}
// }

// class CalisanAdresi
// {
//      public int Id {get; set;}
//      public string Adi {get; set;}
//      public Calisan Calisan {get; set;}
// }
//public class ExampleDb2Context : DbContext
//{
//    public DbSet<Product> Products { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
//    public DbSet<Customer> Customers { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // bu fonksiyon context nesnemiz ile ilgili temek konfigürasyonları yapmamızı sağlar. Bir alt regionda teorik bilgiler bulunmaktadır.
//    {
//        // burada Database daha önce yoksa sizin yazdığınız şekilde bir database(ECommerceDbContext) oluşturur.
//        //Provider yapılandırılması
//        // ConnectionString yapılandırılması
//        // Lazy Loading
//        // vb.
//        optionsBuilder.UseSqlServer("server=localhost; User Id=onur; Database=ECommerceDbContext; Password=xAJa7bhu*D2g; Trusted_Connection=True; TrustServerCertificate=True");

//    }
//    // Modellerin(entity) veritabanında generate edilecek yapıları bu fonksiyonda içerinde konfigüre edilir.
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        //modelBuilder.Entity<CalisanAdresi>()
//        //    .HasKey(a => a.Id);

//        //modelBuilder.Entity<Calisan>() bu fluent API
//        //    .HasOne(c => c.CalisanAdresi)
//        //    .WithOne(c => c.Calisan)
//        //    .HasForeignKey<CalisanAdresi>(c => c.Id);
//    }
//}


#endregion

#region One to Many Relationship | Tüm Detaylarıyla Bire Çok İlişki Yapılanması
// Default Convention
//burada dependent entity Calisan dır.
//Alttaki haliyle EF Core bu durumu anlar.
// Default convention yönteminde bire ok ilişkiyi kurarken foreign key kolonuna karşılık gelen bir property tanımlamak mecburiyetinde değiliz.
// Eğer tanımlamazsak EF Core bunu kendisi tamamlayacak yok eğer tanımlarsak , anımladığımızı baz alacaktır.
//class Calisan
//{
//    public int Id { get; set; }
//    //public int DepartmanId { get; set; } istersen bunu yaparsın istersen eklemezsin 
//    public string DepartmanAdi { get; set; }
//    public Departman Departman { get; set; }
//}

//class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public List<Calisan> Calisan { get; set; }
//}
// Data Annotations
// Default convention yönteminde foreign key kolonuna karşılık gelen property'i tanımladığımızda 
// bu property ismi temel geleneksel entity tanımlama kurallarına uymuyorsa eğer Data Annotations'lar ile müdahalede bulunabiliriz.
//class Calisan
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Departman))]
//    public int DepartmanId { get; set; }
//    public string DepartmanAdi { get; set; }
//    public Departman Departman { get; set; }
//}

//class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public List<Calisan> Calisan { get; set; }
//}
// Fluent API
class Calisan
{
    public int Id { get; set; }
    [ForeignKey(nameof(Departman))]
    public int DepartmanId { get; set; }
    public string DepartmanAdi { get; set; }
    public Departman Departman { get; set; }
}

class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public List<Calisan> Calisanlar { get; set; }
}
public class ExampleDb2Context : DbContext
{
    public DbSet<Product> Products { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
    public DbSet<Customer> Customers { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // bu fonksiyon context nesnemiz ile ilgili temek konfigürasyonları yapmamızı sağlar. Bir alt regionda teorik bilgiler bulunmaktadır.
    {
        // burada Database daha önce yoksa sizin yazdığınız şekilde bir database(ECommerceDbContext) oluşturur.
        //Provider yapılandırılması
        // ConnectionString yapılandırılması
        // Lazy Loading
        // vb.
        optionsBuilder.UseSqlServer("server=localhost; User Id=onur; Database=ECommerceDbContext; Password=xAJa7bhu*D2g; Trusted_Connection=True; TrustServerCertificate=True");

    }
    // Modellerin(entity) veritabanında generate edilecek yapıları bu fonksiyonda içerinde konfigüre edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<CalisanAdresi>()
        //    .HasKey(a => a.Id);

        //modelBuilder.Entity<Calisan>() bu fluent API
        //    .HasOne(c => c.Departman)
        //    .WithMany(c => c.Calisanlar); Foreign Keye gerek yok kendisi oluşturur eğer Entity de tanımladıysanız yazmanız gerekir.
    }
}

#endregion


#region Many to Many Realtionship | Tüm Detaylarıyla Çoka Çok İlişki Yapılanması

// Default Convention
// İki entity arasındaki ilişkiyi navigation propertyler üzerinden çoğul olarak kurmalıyız.
// (ICollection , List)
// Default Convention'da cross table'ı manuel oluşturmak zorunda değiliz. EF Core tasarıma uygun bir şekilde cross table'ı kendisi otomatik basacak ve generate edecektir.
// Ve oluşturulan cross table'ın içerisinde composite primary key'i de otomatik oluşturmuş oacaktır.
//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<Yazar> Yazarlar { get; set; }
//}

//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<Kitap> Kitaplar { get; set; }
//}



// Data Annotations
// cross table manuel olarak oluşturulmak zorundadır.
// Entity'lerde oluşturduğumuz cross table entitysi ile bire çok bir ilişki kurulmalı.
// Cross table ' ın illaki context nesnesi içinde DbSet şeklinde tanımlanmasına gerek yok.
//public class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<KitapYazar> Yazarlar { get; set; }
//}

//public class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<KitapYazar> Kitaplar { get; set; }
//}
//// Cross table
//class KitapYazar
//{
//    //[Key] bu çalışmaz 2 tane key koymak. CrossTable'da composite key'i data annotation(Attributes)lar ile manuel kuramıyoruz. Bunun 
//    // için de Fluent API'da çalışma yapmamız gerekiyor.
//    [ForeignKey(nameof(Yazar))] // eğer farklı bir isim verirseniz foreign keye bunu ef core anlamaz bu durumda bu şekilde attribute'ü kullanmanız gerekir.
//    public int YazarId { get; set; }
//    //[Key]
//    [ForeignKey(nameof(Kitap))]
//    public int KitapId { get; set; }
//    public Kitap Kitap { get; set; }
//    public Yazar Yazar { get; set; }    
//}

//public class EKitapDbContext : DbContext
//{
//    public DbSet<Kitap> Kitaplar { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
//    public DbSet<Yazar> Yazarlar { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // bu fonksiyon context nesnemiz ile ilgili temek konfigürasyonları yapmamızı sağlar. Bir alt regionda teorik bilgiler bulunmaktadır.
//    {
//        // burada Database daha önce yoksa sizin yazdığınız şekilde bir database(ECommerceDbContext) oluşturur.
//        //Provider yapılandırılması
//        // ConnectionString yapılandırılması
//        // Lazy Loading
//        // vb.
//        optionsBuilder.UseSqlServer("server=localhost; User Id=onur; Database=ECommerceDbContext; Password=xAJa7bhu*D2g; Trusted_Connection=True; TrustServerCertificate=True");

//    }
//    // Modellerin(entity) veritabanında generate edilecek yapıları bu fonksiyonda içerinde konfigüre edilir.
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<KitapYazar>()
//            .HasKey(ky => new {ky.KitapId,ky.YazarId}); // bu şekilde compossite primary key oluşturuluyor.
//    }
//}

// Fluent API
// Cross table manuel oluşturulmalı, DbSet olarak eklenmesine lüzum yok,
// Composite PK Haskey metodu ile kurulmalı.


public class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public ICollection<KitapYazar> Yazarlar { get; set; }
}

public class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<KitapYazar> Kitaplar { get; set; }
}
// Cross table
public class KitapYazar
{
    //[Key] bu çalışmaz 2 tane key koymak. CrossTable'da composite key'i data annotation(Attributes)lar ile manuel kuramıyoruz. Bunun 
    // için de Fluent API'da çalışma yapmamız gerekiyor.
    [ForeignKey(nameof(Yazar))] // eğer farklı bir isim verirseniz foreign keye bunu ef core anlamaz bu durumda bu şekilde attribute'ü kullanmanız gerekir.
    public int YazarId { get; set; }
    //[Key]
    [ForeignKey(nameof(Kitap))]
    public int KitapId { get; set; }
    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }
}

public class EKitapDbContext : DbContext
{
    public DbSet<Kitap> Kitaplar { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
    public DbSet<Yazar> Yazarlar { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // bu fonksiyon context nesnemiz ile ilgili temek konfigürasyonları yapmamızı sağlar. Bir alt regionda teorik bilgiler bulunmaktadır.
    {
        // burada Database daha önce yoksa sizin yazdığınız şekilde bir database(ECommerceDbContext) oluşturur.
        //Provider yapılandırılması
        // ConnectionString yapılandırılması
        // Lazy Loading
        // vb.
        optionsBuilder.UseSqlServer("server=localhost; User Id=onur; Database=ECommerceDbContext; Password=xAJa7bhu*D2g; Trusted_Connection=True; TrustServerCertificate=True");

    }
    // Modellerin(entity) veritabanında generate edilecek yapıları bu fonksiyonda içerinde konfigüre edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky => new { ky.KitapId, ky.YazarId }); // bu şekilde compossite primary key oluşturuluyor.

        modelBuilder.Entity<KitapYazar>()
            .HasOne(x => x.Kitap)
            .WithMany(x => x.Yazarlar);

        modelBuilder.Entity<KitapYazar>()
            .HasOne(x => x.Yazar)
            .WithMany(x => x.Kitaplar);

    }
}
#endregion

#region İlişkisel Senaryolarda Veri Ekleme Davranışları

#region One to One İlişkisel Senaryolarda Veri Ekleme

//1. Yöntem Principal Entity Üzerinden Dependent Entity Verisi Ekleme

//Person personad = new Person();

//personad.Name = "Test";
//personad.Address = new() { PersonAddress = "Ankara" };

//await context.AddAsync();
//await context.SaveChangesAsync(); bu şekilde 2 sine de kayıt atacaktır.

// 2.Yöntem -> Dependent Entity Üzerinden Principal Entity Verisi Ekleme
//Address address = new Address()
//{
//    PersonAddress = "Test",
//    Person = new() { Name = "TestOnur" }
//};
// awit context.AddAsync(address);
// await context.SaveChangesAsync(); buradada ilk principle entitiyi ekler.

// Eğer ki principal entity üzerinden ekleme gerçekleştiriliyorsa dependent entity nesnesi verilmek zorunda değildir! amma velakin,
// dependent entity üzerinden ekleme işlemi gerçekleştiriliyorsa eğer burada principal entitynin nesnesine ihtiyacımız zaruridir.
//public class Address
//{
//    public int Id {  set; get; }
//    public string PersonAddress { get; set; }
//    public Person Person { get; set; }
//}
//public class RelationContext : DbContext
//{




//}

//public class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public Address Address { get; set; }
//}

#endregion

#region One to Many İlişkisel Senaryolarda Veri Ekleme
// Nesne Referansı Üzerinden Ekleme
// Blog blog = new() { Name ="Gencayyildiz.com Blog" };
// blog.Posts.Add(new() {Title ="Post 1" });
// blog.Posts.Add(new() {Title ="Post 2" });
// context.AddAsync(blog);
// await context.SaveChangesAsync();

// Object Initializer Üzerinden Ekleme

// Blog blog = new() { Name ="Gencayyildiz.com Blog",
//  Posts = HashSet<Post> {new(){Title ="Test"} , new() {Title ="Test2"}};
// };
// blog.Posts.Add(new() {Title ="Post 1" });
// blog.Posts.Add(new() {Title ="Post 2" });
// context.AddAsync(blog);
// await context.SaveChangesAsync();

// 2. Yöntem -> Dependent Entity Üzerinden Principal Entity Verisi Ekleme
// aykırı bir yöntemdir.
// Post post = new()
//{
// Title ="Post 6",
// Blog = new() {Name ="B Blog"
//}
//class Blog
//{
//    public Blog()
//    {
//        Posts = new HashSet<Post>();
//    }
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Post> Posts { get; set; }
//}

//class Post
//{
//    public int Id { get; set; }
//    public int BlogId { get; set; }
//    public string Title { get; set; }
//    public Blog Blog { get; set; }
//}

// 3. Yöntem -> Foreign Key Kolonu Üzerinden Veri Ekleme

// 1. ve 2. yöntemler hiç olmayan verilerin ilişkisel olarak eklenmesini sağlarken , bu 3. yöntem
// önceden eklenmiş olan bir principal entity verisiyle yeni dependent entitylerin eşleştirmesini sağlanmaktadır.

//Post post = new()
//{
//BlogId = 1,
//Title ="Post 7"
//};

//await context.AddAsync(post);
//await context.SaveChangesAsync();

//class Blog
//{
//    public Blog()
//    {
//        Posts = new HashSet<Post>();
//    }
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Post> Posts { get; set; }
//}

//class Post
//{
//    public int Id { get; set; }
//    public int BlogId { get; set; }
//    public string Title { get; set; }
//    public Blog Blog { get; set; }
//}



#endregion

#region Many to Many İlişkisel Senaryolarda Veri Ekleme
// 1. Yöntem
// n to n ilişkisi eğer ki defautl convention üzerinden tasarlanmışsa kullanılan bir yöntemdir.

// Book book = new()
//{
// BookName = "A Kitabı",
// Authors = new HashSet<Author>()
// {
//      new() {AutherName ="Hilmi"},
//      new() {AutherName ="Ayşe"}
// }
//}
// context.Add(book);
// context.SaveChanges();


//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<Yazar> Yazarlar { get; set; }
//}

//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<Kitap> Kitaplar { get; set; }
//}



// 2. Yöntem
// n to n ilişkisi eğer ki fluent api üzerinden tasarlanmışsa kullanılan bir yöntemdir.

// Yazar yazar = new()
// {
//      AutherName ="Mustafa",
//      Books = new HashSet<BookAuthor>() {
//          new() {BookId = 1} ,
//          new()  { Book = new() { BookName="B Kitap" } }

//
//      }
// }
//
// context.Add(yazar);
// context.SaveChanges();


//public class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<KitapYazar> Yazarlar { get; set; }
//}

//public class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<KitapYazar> Kitaplar { get; set; }
//}
//// Cross table
//public class KitapYazar
//{
//    //[Key] bu çalışmaz 2 tane key koymak. CrossTable'da composite key'i data annotation(Attributes)lar ile manuel kuramıyoruz. Bunun 
//    // için de Fluent API'da çalışma yapmamız gerekiyor.
//    [ForeignKey(nameof(Yazar))] // eğer farklı bir isim verirseniz foreign keye bunu ef core anlamaz bu durumda bu şekilde attribute'ü kullanmanız gerekir.
//    public int YazarId { get; set; }
//    //[Key]
//    [ForeignKey(nameof(Kitap))]
//    public int KitapId { get; set; }
//    public Kitap Kitap { get; set; }
//    public Yazar Yazar { get; set; }
//}
#endregion

#endregion

#region İlişkisel Senaryolarda Veri Güncelleme Davranışları
ApplicationDbContext context = new();

#region One to One İlişkisel Senaryolarda Veri Güncelleme
#region Saving
//Person person = new()
//{
//    Name = "Gençay",
//    Address = new()
//    {
//        PersonAddress = "Yenimahalle/ANKARA"
//    }
//};

//Person person2 = new()
//{
//    Name = "Hilmi"
//};

//await context.AddAsync(person);
//await context.AddAsync(person2);
//await context.SaveChangesAsync();
#endregion

#region 1. Durum | Esas tablodaki veriye bağımlı veriyi değiştirme
//Person? person = await context.Persons
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p => p.Id == 1);

//context.Addresses.Remove(person.Address);
//person.Address = new()
//{
//    PersonAddress = "Yeni adres"
//};

//await context.SaveChangesAsync();
#endregion
#region 2. Durum | Bağımlı verinin ilişkisel olduğu ana veriyi güncelleme
//Address? address = await context.Addresses.FindAsync(1);
//address.Id = 2;
//await context.SaveChangesAsync();

//Address? address = await context.Addresses.FindAsync(2);
//context.Addresses.Remove(address);
//await context.SaveChangesAsync();

////Person? person = await context.Persons.FindAsync(2);
////address.Person = person;

//address.Person = new()
//{
//    Name = "Rıfkı"
//};
//await context.Addresses.AddAsync(address);

//await context.SaveChangesAsync();
#endregion
#endregion

#region One to Many İlişkisel Senaryolarda Veri Güncelleme
#region Saving
//Blog blog = new()
//{
//    Name = "Gencayyildiz.com Blog",
//    Posts = new List<Post>
//    {
//        new(){ Title = "1. Post" },
//        new(){ Title = "2. Post" },
//        new(){ Title = "3. Post" },
//    }
//};

//await context.Blogs.AddAsync(blog);
//await context.SaveChangesAsync();
#endregion

#region 1. Durum | Esas tablodaki veriye bağımlı verileri değiştirme
//Blog? blog = await context.Blogs
//    .Include(b => b.Posts)
//    .FirstOrDefaultAsync(b => b.Id == 1);

//Post? silinecekPost = blog.Posts.FirstOrDefault(p => p.Id == 2);
//blog.Posts.Remove(silinecekPost);

//blog.Posts.Add(new() { Title = "4. Post" });
//blog.Posts.Add(new() { Title = "5. Post" });

//await context.SaveChangesAsync();
#endregion
#region 2. Durum | Bağımlı verilerin ilişkisel olduğu ana veriyi güncelleme
//Post? post = await context.Posts.FindAsync(4);
//post.Blog = new()
//{
//    Name = "2. Blog"
//};
//await context.SaveChangesAsync();


//Post? post = await context.Posts.FindAsync(5);
//Blog? blog = await context.Blogs.FindAsync(2);
//post.Blog = blog;
//await context.SaveChangesAsync();
#endregion
#endregion

#region Many to Many İlişkisel Senaryolarda Veri Güncelleme
#region Saving
//Book book1 = new() { BookName = "1. Kitap" };
//Book book2 = new() { BookName = "2. Kitap" };
//Book book3 = new() { BookName = "3. Kitap" };

//Author author1 = new() { AuthorName = "1. Yazar" };
//Author author2 = new() { AuthorName = "2. Yazar" };
//Author author3 = new() { AuthorName = "3. Yazar" };

//book1.Authors.Add(author1);
//book1.Authors.Add(author2);

//book2.Authors.Add(author1);
//book2.Authors.Add(author2);
//book2.Authors.Add(author3);

//book3.Authors.Add(author3);

//await context.AddAsync(book1);
//await context.AddAsync(book2);
//await context.AddAsync(book3);
//await context.SaveChangesAsync();
#endregion

#region 1. Örnek
//Book? book = await context.Books.FindAsync(1);
//Author? author = await context.Authors.FindAsync(3);
//book.Authors.Add(author);

//await context.SaveChangesAsync();
#endregion
#region 2. Örnek
//Author? author = await context.Authors
//    .Include(a => a.Books)
//    .FirstOrDefaultAsync(a => a.Id == 3);

//foreach (var book in author.Books)
//{
//    if (book.Id != 1)
//        author.Books.Remove(book);
//}

//await context.SaveChangesAsync();
#endregion
#region 3. Örnek
//Book? book = await context.Books
//    .Include(b => b.Authors)
//    .FirstOrDefaultAsync(b => b.Id == 2);

//Author silinecekYazar = book.Authors.FirstOrDefault(a => a.Id == 1);
//book.Authors.Remove(silinecekYazar);

//Author author = await context.Authors.FindAsync(3);
//book.Authors.Add(author);
//book.Authors.Add(new() { AuthorName = "4. Yazar" });

//await context.SaveChangesAsync();
#endregion
#endregion


class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
}
class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }

    public Person Person { get; set; }
}
class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }

    public Blog Blog { get; set; }
}
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<Author> Authors { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<Book> Books { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);
    }
}


#endregion




#region Code First Yaklaşımı
/*
 * Code-First Approach Create Database from the Domain Classes
 * Migration & Migrate Kavramları nelerdir
 * Migration sayesinde projemizde modellediğimiz veritabanını veritabnı sunucusunun anladığı dile çevirir. Up ve Down fonksiyonları vardır.
 * Migrate ise göç ettirmek demektir.
 * Migration Oluşturmak için gerekli temek gereksinimler 
 * DbcONTEXT VE Entity classları olması gerekir.
 * Migration Tools kütüphanesi sayesinde oluşturuluyor.
 * Package Manager Console
 * add-migration [Migration-Name]
 * Dotnet CLI
 * dotnet ef migrations add [Migration Name]
 * 
 * bu kodları yazdıktan sonra 1 tane Migrations altında bir klasör oluşturur ve içinde 2 adet fonksiyon vardır birisi Up diğeri Down'dır
 * Up fonksiyonu inkılapçı Down fonksiyonu irticacıdır birisi yeniye götürür diğeri geriye.
 * Migration Path'i Belirleme
 * PMC
 * add-migration [Migration Name] -OutputDir [Path]
 * dotnet ef migrations add [Migration Name] --output-dir [Path]
 * MİGRATİON SİLME
 * PMC : remove-migration 
 * dotnet CLI: dotnet ef migrations remove
 * get-migration bütün migrationları gösterir
 * 
 * Migrationları MİGRATE ETME (UP FONKSİYONU)
 * PMC
 * update-database
 * DOTNET CLI 
 *dotnet ef database update
 *
 *1. migration bütün tabloları oluşturur daha sonraları farkları yakalar.
 *ek olarka __EFMigrationHistory diye bir tablo oluşturur.
 * 
 * Migrationları GERİ ALMA (DOWN FONKSİYONU)
 * update-database mig_1 mig 1 e geri dön demek
 * 
 * Kod Üzerinden Migrate Operasyonu
 * Migrationları tool aracılığıyla migrate edebildiğimiz gibi kod üzerinden de uygulamanın ayakta olduğu süreçte(runtime'da) veritabanını migrate edebiliyoruz.
 * ExampleDbContext context = new();
 * await context.Database.MigrateAsync();
 * Bu nerede işimize yarar örneğin başka bir sunucuya koyduk bunu kullanıcının database'i oluşturmasına gerek yok bu otomatik olarak oluşturuyor. Runtime'da veritabanını migrate eder.
 * Veritabanını sınıfları üzerinde yapılan tüm değişiklikleri migration eşliğinde gönderiniz. Böylece her bir değişiklikleri migration'lar ile kayıt altına almış olursunuz(bu da size veritabanı gelişim sürecini verir)
 * ve ihtiyaca binaen istediğiniz noktaya geri dönüş sağlayabilirsiniz.
 * 
 * Migrationlara mümkün mertebe dokunmamak lazım. Lakin ileride ihtiyaç doğrultusunda ham sql cümlecikleri ekleyeceğimiz ve hatta Stored Procedure gibi yapıları oluşturacağımız noktalar olacaktır.
 */


public class ExampleDb3Context : DbContext
{
    public DbSet<Product> Products { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
    public DbSet<Customer> Customers { get; set; } // Bu şekilde bildirmezsen Entity olduğunu anlamaz 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // bu fonksiyon context nesnemiz ile ilgili temek konfigürasyonları yapmamızı sağlar. Bir alt regionda teorik bilgiler bulunmaktadır.
    {
        // burada Database daha önce yoksa sizin yazdığınız şekilde bir database(ECommerceDbContext) oluşturur.
        //Provider yapılandırılması
        // ConnectionString yapılandırılması
        // Lazy Loading
        // vb.
        optionsBuilder.UseSqlServer("server=localhost; User Id=onur; Database=ECommerceDbContext; Password=xAJa7bhu*D2g; Trusted_Connection=True; TrustServerCertificate=True");

    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int UnitCount { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public int LastName { get; set; }
}

#endregion


#region En Temel Entity Kuralı 
// OnConfiguring İle Konfigürasyon Ayarlarını Gerçekleştirmek
// EF Core tool'unu yapılandırmak için kullandığımız bir metottur.
// Context nesnesinde override edilerek kullanılmaktadır.
// 

//Basit Düzeyde Entity Tanımlama Kuralları 
// EF Core Entitylerde default olarak bir primary key beklemektedir.
// Haliyle, bu kolonu temsil eden bir property tanımlamadığımız takdirde hata verecektir.
// örneğin Urun adında bir Entityimiz var : bir property'in Primary Key olabilmesi için Id , ID , UrunId , UrunID bunlar default olarak primary Keydir.
// DbSet <Urun> Urunler -> bu tablo isim oluyor.
#endregion


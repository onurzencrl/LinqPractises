
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;
using System.Reflection;

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


#region İlişkisel Senaryolarda Veri Silme Davranışları

//ApplicationDbContext context = new();

//#region One to One İlişkisel Senaryolarda Veri Silme
////Person? person = await context.Persons
////    .Include(p => p.Address)
////    .FirstOrDefaultAsync(p => p.Id == 1);

////if (person != null)
////    context.Addresses.Remove(person.Address);
////await context.SaveChangesAsync();
//#endregion

//#region One to Many İlişkisel Senaryolarda Veri Silme
////Blog? blog = await context.Blogs
////    .Include(b => b.Posts)
////    .FirstOrDefaultAsync(b => b.Id == 1);
////Post? post = blog.Posts.FirstOrDefault(p => p.Id == 2);

////context.Posts.Remove(post);
////await context.SaveChangesAsync();
//#endregion

//#region Many to Many İlişkisel Senaryolarda Veri Silme
////Book? book = await context.Books
////    .Include(b => b.Authors)
////    .FirstOrDefaultAsync(b => b.Id == 2);

////Author? author = book.Authors.FirstOrDefault(a => a.Id == 2);
////context.Authors.Remove(author); //Yazarı silmeye kalkar!!!
//////book.Authors.Remove(author);
////await context.SaveChangesAsync();
//#endregion

//#region Cascade Delete
////Bu davranış modelleri Fluent API ile konfigüre edilebilmektedir.
//#region Cascade
////Esas tablodan silinen veriyle karşı/bağımlı tabloda bulunan ilişkili verilerin silinmesini sağlar.
//#endregion

//#region SetNull
////Esas tablodan silinen veriyle karşı/bağımlı tabloda bulunan ilişkili verilere null değerin atanmasını sağlar.

////One to One senaryolarda eğer ki Foreign key ve Primary key kolonları aynı ise o zaman SetNull davranuışını KULLANAMAYIZ!
//#endregion

//#region Restrict
////Esas tablodan herhangi bir veri silinmeye çalışıldığında o veriye karşılık dependent table'da ilişkisel veri/ler varsa eğer bu sefer bu silme işlemini engellemesini sağlar.
//#endregion

//Blog? blog = await context.Blogs.FindAsync(2);
//context.Blogs.Remove(blog);
//await context.SaveChangesAsync();
//#endregion

//#region Saving Data
////Person person = new()
////{
////    Name = "Gençay",
////    Address = new()
////    {
////        PersonAddress = "Yenimahalle/ANKARA"
////    }
////};

////Person person2 = new()
////{
////    Name = "Hilmi"
////};

////await context.AddAsync(person);
////await context.AddAsync(person2);

////Blog blog = new()
////{
////    Name = "Gencayyildiz.com Blog",
////    Posts = new List<Post>
////    {
////        new(){ Title = "1. Post" },
////        new(){ Title = "2. Post" },
////        new(){ Title = "3. Post" },
////    }
////};

////await context.Blogs.AddAsync(blog);

////Book book1 = new() { BookName = "1. Kitap" };
////Book book2 = new() { BookName = "2. Kitap" };
////Book book3 = new() { BookName = "3. Kitap" };

////Author author1 = new() { AuthorName = "1. Yazar" };
////Author author2 = new() { AuthorName = "2. Yazar" };
////Author author3 = new() { AuthorName = "3. Yazar" };

////book1.Authors.Add(author1);
////book1.Authors.Add(author2);

////book2.Authors.Add(author1);
////book2.Authors.Add(author2);
////book2.Authors.Add(author3);

////book3.Authors.Add(author3);

////await context.AddAsync(book1);
////await context.AddAsync(book2);
////await context.AddAsync(book3);
////await context.SaveChangesAsync();
//#endregion

//class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public Address Address { get; set; }
//}
//class Address
//{
//    public int Id { get; set; }
//    public string PersonAddress { get; set; }

//    public Person Person { get; set; }
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
//    public int? BlogId { get; set; }
//    public string Title { get; set; }

//    public Blog Blog { get; set; }
//}
//class Book
//{
//    public Book()
//    {
//        Authors = new HashSet<Author>();
//    }
//    public int Id { get; set; }
//    public string BookName { get; set; }

//    public ICollection<Author> Authors { get; set; }
//}
//class Author
//{
//    public Author()
//    {
//        Books = new HashSet<Book>();
//    }
//    public int Id { get; set; }
//    public string AuthorName { get; set; }

//    public ICollection<Book> Books { get; set; }
//}


//class ApplicationDbContext : DbContext
//{
//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Address> Addresses { get; set; }
//    public DbSet<Post> Posts { get; set; }
//    public DbSet<Blog> Blogs { get; set; }
//    public DbSet<Book> Books { get; set; }
//    public DbSet<Author> Authors { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Address>()
//            .HasOne(a => a.Person)
//            .WithOne(p => p.Address)
//            .HasForeignKey<Address>(a => a.Id);

//        modelBuilder.Entity<Post>()
//            .HasOne(p => p.Blog)
//            .WithMany(b => b.Posts)
//            .IsRequired(false)
//            .OnDelete(DeleteBehavior.Restrict);

//        modelBuilder.Entity<Book>()
//            .HasMany(b => b.Authors)
//            .WithMany(a => a.Books);
//    }
//}
#endregion

#region Backing Fields
BackingFieldDbContext context = new();

var person = await context.Persons.FindAsync(1);
//Person person2 = new()
//{
//    Name = "Person 101",
//    Department = "Department 101"
//};

//await context.Persons.AddAsync(person2);
//await context.SaveChangesAsync();

Console.Read();

#region Backing Fields
//Tablo içerisindeki kolonları, entity class'ları içerisinde property'ler ile değil field'larla temsil etmemizi sağlayan bir özelliktir.
//class Person
//{
//    public int Id { get; set; }
//    public string name;
//    public string Name { get => name.Substring(0, 3); set => name = value.Substring(0, 3); }
//    public string Department { get; set; }
//}
#endregion

#region BackingField Attributes
//class Person
//{
//    public int Id { get; set; }
//    public string name;
//    [BackingField(nameof(name))]
//    public string Name { get; set; }
//    public string Department { get; set; }
//}
#endregion

#region HasField Fluent API
//Fleunt API'da HasField metodu BackingField özelliğine karşılık gelmektedir.
//class Person
//{
//    public int Id { get; set; }
//    public string name;
//    public string Name { get; set; }
//    public string Department { get; set; }
//}
#endregion

#region Field And Property Access
//EF Core sorgulama sürecinde entity içerisindeki propertyleri ya da field'ları kullanıp kullanmayacağının davranışını bizlere belirtmektedir.

//EF Core, hiçbir ayarlama yoksa varsayılan olarak propertyler üzerinden verileri işler, eğer ki backing field bildiriliyorsa field üzerinden işler yok eğer backing field bildirildiği halde davranış belirtiliyorsa ne belirtilmişse ona göre işlemeyi devam ettirir.

//UsePropertyAccessMode üzerinden davranış modellemesi gerçekleştirilebilir.
#endregion

#region Field-Only Properties
//Entitylerde değerleri almak için property'ler yerine metotların kullanıldığı veya belirli alanların hiç gösterilmemesi gerektiği durumlarda(örneğin primary key kolonu) kullanabilir.
class Person
{
    public int Id { get; set; }
    public string name;
    public string Department { get; set; }

    public string GetName()
        => name;
    public string SetName(string value)
        => this.name = value;
}
#endregion

class BackingFieldDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=BaskingFieldDb;User ID=SA;Password=1q2w3e4r+!");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Name)
        //    .HasField(nameof(Person.name))
        //    .UsePropertyAccessMode(PropertyAccessMode.PreferProperty);

        //Field : Veri erişim süreçlerinde sadece field'ların kullanılmasını söyler. Eğer field'ın kullanılamayacağı durum söz konusu olursa bir exception fırlatır.
        //FieldDuringConstruction : Veri erişim süreçlerinde ilgili entityden bir nesne oluşturulma sürecinde field'ların kullanılmasını söyler.,
        //Property : Veri erişim sürecinde sadece propertynin kullanılmasını söyler. Eğer property'nin kullanılamayacağı durum söz konusuysa (read-only, write-only) bir exception fırlatır.
        //PreferField,
        //PreferFieldDuringConstruction,
        //PreferProperty

        modelBuilder.Entity<Person>()
            .Property(nameof(Person.name));
    }
}

#endregion

#region Shadow Property 

ApplicationDbContext context = new();

#region Shadow Properties - Gölge Özellikler
//Entity sınıflarında fiziksel olarak tanımlanmayan/modellenmeyen ancak EF Core tarafından ilgili entity için var olan/var olduğu kabul edilen property'lerdir.
//Tabloda gösterilmesini istemediğimiz/lüzumlu görmediğimiz/entity instance'ı üzerinde işlem yapmayacağımız kolonlar için shadow propertyler kullanılabilir.
//Shadow property'lerin değerleri ve stateleri Change Tracker tarafından kontrol edilir.
#endregion

#region Foreign Key - Shadow Properties
//İlişkisel senaryolarda foreign key property'sini tanımlamadığımız halde EF Core tarafından dependent entity'e eklenmektedir. İşte bu shadow property'dir.

//var blogs = await context.Blogs.Include(b => b.Posts)
//    .ToListAsync();
//Console.WriteLine();
#endregion

#region Shadow Property Oluşturma
//Bir entity üzerinde shadow property oluşturmak istiyorsanız eğer Fluent API'ı kullanmanız gerekmektedir.
//        modelBuilder.Entity<Blog>()
//            .Property<DateTime>("CreatedDate");
#endregion

#region Shadow Property'e Erişim Sağlama
#region ChangeTracker İle Erişim
//Shadow property'e erişim sağlayabilmek için Change Tracker'dan istifade edilebilir.

//var blog = await context.Blogs.FirstAsync();

//var createDate = context.Entry(blog).Property("CreatedDate");
//Console.WriteLine(createDate.CurrentValue);
//Console.WriteLine(createDate.OriginalValue);

//createDate.CurrentValue = DateTime.Now;
//await context.SaveChangesAsync();
#endregion

#region EF.Property İle Erişim
//Özellikle LINQ sorgularında Shadow Propery'lerine erişim için EF.Property static yapılanmasını kullanabiliriz.
//var blogs = await context.Blogs.OrderBy(b => EF.Property<DateTime>(b, "CreatedDate")).ToListAsync();

//var blogs2 = await context.Blogs.Where(b => EF.Property<DateTime>(b, "CreatedDate").Year > 2020).ToListAsync();
//Console.WriteLine();
#endregion
#endregion


class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}

class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool lastUpdated { get; set; }

    public Blog Blog { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property<DateTime>("CreatedDate");

        base.OnModelCreating(modelBuilder);
    }
}
#endregion


#region EF Core Konfigürasyonları
ApplicationDbContext context = new();

#region EF Core'da Neden Yapılandırmalara İhtiyacımız Olur?
//Default davranışları yeri geldiğinde geçersiz kılmak ve özelleştirmek isteyebiliriz. Bundan dolayı yapılandırmalara ihtiyacımız olacaktır.
#endregion

#region OnModelCreating Metodu
//EF Core'da yapılandırma deyince akla ilk gelen metot OnModelCreating metodudur.
//Bu metot, DbContext sınıfı içerisinde virtual olarak ayarlanmış bir metottur.
//Bizler bu metodu kullanarak model'larımızla ilgili temel konfigürasyonel davranışları(Fluent API) sergileyeibliriz.
//Bir model'ın yaratılışıyla ilgili tüm konfigürasyonları burada gerçekleştirebilmekteyiz.

#region GetEntityTypes
//EF Core'da kullanılan entity'leri elde etmek, programatik olarak öğrenmek istiyorsak eğer GetEntityTypes fonksiyonunu kullanabiliriz.
#endregion

#endregion

#region Configurations | Data Annotations & Fluent API

#region Table - ToTable
//Generate edilecek tablonun ismini belirlememizi sağlayan yapılandırmadır.
//Ef Core normal şartlarda generate edeceği tablonun adını DbSet property'sinden almaktadır. Bizler eğer ki bunu özelleştirmek istiyorsak Table attribute'unu yahut ToTable api'ını kullanabilriiz.
#endregion

#region Column - HasColumnName, HasColumnType, HasColumnOrder
//EF Core'da tabloların kolonları entity sınıfları içerisindeki property'lere karşılık gelmektedir. 
//Default olarak property'lerin adı kolon adıyken, türleri/tipleri kolon türleridir.
//Eğer ki generate edilecek kolon isimlerine ve türlerine müdahale etmek sitiyorsak bu konfigürasyon kullanılır.
#endregion

#region ForeignKey - HasForeignKey
//İlişkisel tablo tasarımlarında, bağımlı tabloda esas tabloya karşılık gelecek verilerin tutulduğu kolonu foreign key olarak temsil etmekteyiz.
//EF Core'da foreign key kolonu genellikle Entity Tnaımlama kuralları gereği default yapılanmalarla oluşturulur.
//ForeignKey Data Annotations Attribute'unu direkt kullanabilirsiniz. Lakin Fluent api ile bu konfigürasyonu yapacaksanız iki entity arasındaki ilişkiyide modellemeniz gerekmektedir. Aksi taktirde fluent api üzerinde HasForeignKey fonksiyonunu kullanamnazsınız!
#endregion

#region NotMapped - Ignore
//EF Core, entity sınıfları içerisindeki tüm proeprtyleri default olarak modellenen tabloya kolon şeklinde migrate eder.
//Bazn bizler entity sınıfları içerisinde tabloda bir kolona karşılık gelmeyen propertyler tanımlamak mecburiyetinde kalabiliriz.
//Bu property'lerin ef core tarafından kolon olarak map edilmesini istemediğimizi bildirebilmek için NotMapped ya da Ignore kullanabiliriz.
#endregion

#region Key - HasKey
//EF Core'da, default convention olarak bir entity'nin içerisinde Id, ID, EntityId, EntityID vs. şeklinde tanımlanan tüm proeprtylere varsayılan olarak primary key constraint uygulanır.
//Key ya da HasKey yapılanmalarıyla istediğinmiz her hangi bir proeprty'e default convention dışında pk uygulayabiliriz.
//EF Core'da bir entity içerisinde kesinlikle PK'i temsil edecek olan property bulunmalıdır. Aksi taktirde EF Core migration olutşurken hata verecektir. Eğer ki tablonun PK'i yoksa bunun bildirilmesi gerekir. 
#endregion

#region Timestamp - IsRowVersion
//İleride/sonraki derlerde veri tutarlılığı ile ilgili bir ders yapacağız.
//Bu derste bir satırdaki verinin bütünsel olarak değişikliğini takip etmemizi sağlayacak olan verisyon mantığını konuşuyor olacağız.
//İşte bir verinin verisyonunu oluşturmamızı sağlayan yapılanma bu konfigürasyonlardır.
#endregion

#region Required - IsRequired
//Bir kolonun nullable ya da not null olup olmamasını bu konfigürasyonla belirleyebiliriz.
//EF Core'da bir property default oalrak not null şeklinde tanımlanır. Eğer ki property'si nullable yapmak istyorsak türü üzerinde ?(nullable) operatörü ile bbildirimde bulunmamız gerekmektedir.
#endregion

#region MaxLenght | StringLength - HasMaxLength
//Bir kolonun max karakter sayısını belirlememizi sağlar.
#endregion

#region Precision - HasPrecision
//Küsüratlı sayılarda bir kesinlik belirtmemizi ve noktanın hanesini bildirmemizi sağlayan bir yapılandırmadır.
#endregion

#region Unicode - IsUnicode
//Kolon içerisinde unicode karakterler kullanılacaksa bu yapılandırmadan istifade edilebilir.
#endregion

#region Comment - HasComment
//EF Core üzerinden oluşturulmuş olan veritabanı nesneleri üzerinde bir açıkalama/yorum yapmak istiyorsanız Comment'i kullanblirsiniz.
#endregion

#region ConcurrencyCheck - IsConcurrencyToken
//İleride/sonraki derlerde veri tutarlılığı ile ilgili bir ders yapacağız.
//Bu derste bir satırdaki verinin bütünsel olarak tutarlılığını sağlayacak bir concurrency token yapılanmasından bahsececeğiz.
#endregion

#region InverseProperty
//İki entity arasında birden fazla ilişki varsa eğer bu ilişkilerin hangi navigation property üzerinden oılacağını ayarlamamızı sağlayan bir konfigrasyondur.
#endregion

#endregion

#region Configurations | Fluent API

#region Composite Key
//Tablolarda birden fazla kolonu kümülatif olarak primary key yapmak istiyorsak buna composite key denir.
#endregion

#region HasDefaultSchema
//EF Core üzerinden inşa edilen herhangi bir veritabanı nesnesi default olarak dbo şemasına sahiptir. Bunu özelleştirebilmek için kullanılan bir yapılandırmadır.
#endregion

#region Property

#region HasDefaultValue
//Tablodaki herhangi bir kolonun değer gönderilmediği durumlarda default olarak hangi değeri alacağını belirler.
#endregion

#region HasDefaultValueSql
//Tablodaki herhangi bir kolonun değer gönderilmediği durumlarda default olarak hangi sql cümleciğinden değeri alacağını belirler.
#endregion

#endregion

#region HasComputedColumnSql
//Tablolarda birden fazla kolondaki veirleri işleyerek değerini oluşturan kolonlara Computed Column denmektedir. EF Core üzerinden bu tarz computed column oluşturabilmek için kullanıolan bir yapılandırmadır.
#endregion

#region HasConstraintName
//EF Core üzerinden oluşturulkan constraint'lere default isim yerine özelleştirilmiş bir isim verebilmek için kullanılan yapılandırmadır.
#endregion

#region HasData
//Sonraki derslerimizde Seed Data isimli bir konuyu incleyeceğiz. Bu konuda migrate sürecinde veritabanını inşa ederken bir yandan da yazılım üzerinden hazır veriler oluşturmak istiyorsak eğer buunun yöntemini usulünü inceliyor olacağız.
//İşte HasData konfigürasyonu bu operasyonun yapılandırma ayağıdır.
//HasData ile migrate sürecinde oluşturulacak olan verilerin pk olan id kolonlarına iradeli bir şekilde değerlerin girilmesi zorunludur!
#endregion

#region HasDiscriminator
//İleride entityler arasında kalıtımsal ilişkilerin olduğu TPT ve TPH isminde konuları inceliyor olacağız. İşte bu konularla ilgili yapılandırmalarımız HasDiscriminator ve HasValue fonksiyonlarıdır.

#region HasValue

#endregion

#endregion

#region HasField
//Backing Field özelliğini kullanmamızı sağlayan bir yapılandırmadır.
#endregion

#region HasNoKey
//Normal şartlarda EF Core'da tüm entitylerin bir PK kolonu olmak zorundadır. Eğer ki entity'de pk kolonu olmayacaksa bunun bildirilmesi gerekmektedir! İşte bunun için kullanuılan fonksiyondur.
#endregion

#region HasIndex
//Sonraki derslerimizde EF Core üzerinden Index yapılanmasını detaylıca inceliyor olacağız.
//Bu ypılanmaya dair konfigürasyonlarımız HasIndex ve Index attribute'dur.
#endregion

#region HasQueryFilter
//İleride göreceğimiz Global QUery Filter başlıklı dersimizin yapılandırmasıdır.
//Temeldeki görevi bir entitye karşılık uygulama bazında global bir filtre koymaktır.
#endregion

#region DatabaseGenerated - ValueGeneratedOnAddOrUpdate, ValueGeneratedOnAdd, ValueGeneratedNever

#endregion
#endregion



//[Table("Kisiler")]
class Person
{
    //[Key]
    public int Id { get; set; }
    //public int Id2 { get; set; }
    //[ForeignKey(nameof(Department))]
    //public int DId { get; set; }
    //[Column("Adi", TypeName = "metin", Order = 7)]
    public int DepartmentId { get; set; }
    public string _name;
    public string Name { get => _name; set => _name = value; }
    //[Required()]
    //[MaxLength(13)]
    //[StringLength(14)]
    [Unicode]
    public string? Surname { get; set; }
    //[Precision(5, 3)]
    public decimal Salary { get; set; }
    //Yazılımsal amaçla oluşturduğum bir property
    //[NotMapped]
    //public string Laylaylom { get; set; }

    [Timestamp]
    //[Comment("Bu şuna yaramaktadır...")]
    public byte[] RowVersion { get; set; }

    //[ConcurrencyCheck]
    //public int ConcurrencyCheck { get; set; }

    public DateTime CreatedDate { get; set; }
    public Department Department { get; set; }
}
class Department
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Person> Persons { get; set; }
}
class Example
{

    public int X { get; set; }
    public int Y { get; set; }
    public int Computed { get; set; }
}
class Entity
{
    public int Id { get; set; }
    public string X { get; set; }
}
class A : Entity
{
    public int Y { get; set; }
}
class B : Entity
{
    public int Z { get; set; }
}
class ApplicationDbContext : DbContext
{
    //public DbSet<Entity> Entities { get; set; }
    //public DbSet<A> As { get; set; }
    //public DbSet<B> Bs { get; set; }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Department> Departments { get; set; }
    //public DbSet<Flight> Flights { get; set; }
    //public DbSet<Airport> Airports { get; set; }
    public DbSet<Example> Examples { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region GetEntityTypes
        //var entities = modelBuilder.Model.GetEntityTypes();
        //foreach (var entity in entities)
        //{
        //    Console.WriteLine(entity.Name);
        //}
        #endregion
        #region ToTable
        //modelBuilder.Entity<Person>().ToTable("aksdmkasmdk");
        #endregion
        #region Column
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Name)
        //    .HasColumnName("Adi")
        //    .HasColumnType("asldalsd")
        //    .HasColumnOrder(7);
        #endregion
        #region ForeignKey
        //modelBuilder.Entity<Person>()
        //    .HasOne(p => p.Department)
        //    .WithMany(d => d.Persons)
        //    .HasForeignKey(p => p.DId);
        #endregion
        #region Ignore
        //modelBuilder.Entity<Person>()
        //    .Ignore(p => p.Laylaylom);
        #endregion
        #region Primary Key
        //modelBuilder.Entity<Person>()
        //    .HasKey(p => p.Id);
        #endregion
        #region IsRowVersion
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.RowVersion)
        //    .IsRowVersion();
        #endregion
        #region Required
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Surname).IsRequired();
        #endregion
        #region MaxLength
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Surname)
        //    .HasMaxLength(13);
        #endregion
        #region Precision
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Salary)
        //    .HasPrecision(5, 3);
        #endregion
        #region Unicode
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Surname)
        //    .IsUnicode();
        #endregion
        #region Comment
        //modelBuilder.Entity<Person>()
        //        .HasComment("Bu tablo şuna yaramaktadır...")
        //    .Property(p => p.Surname)
        //        .HasComment("Bu kolon şuna yaramaktadır.");
        #endregion
        #region ConcurrencyCheck
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.ConcurrencyCheck)
        //    .IsConcurrencyToken();
        #endregion
        #region CompositeKey
        //modelBuilder.Entity<Person>().HasKey("Id", "Id2");
        //modelBuilder.Entity<Person>().HasKey(p => new { p.Id, p.Id2 });
        #endregion
        #region HasDefaultSchema
        //modelBuilder.HasDefaultSchema("ahmet");
        #endregion
        #region Property
        #region HasDefaultValue
        //modelBuilder.Entity<Person>()
        // .Property(p => p.Salary)
        // .HasDefaultValue(100);
        #endregion
        #region HasDefaultValueSql
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.CreatedDate)
        //    .HasDefaultValueSql("GETDATE()");
        #endregion
        #endregion
        #region HasComputedColumnSql
        //modelBuilder.Entity<Example>()
        //    .Property(p => p.Computed)
        //    .HasComputedColumnSql("[X] + [Y]");
        #endregion
        #region HasConstraintName
        //modelBuilder.Entity<Person>()
        //    .HasOne(p => p.Department)
        //    .WithMany(d => d.Persons)
        //    .HasForeignKey(p => p.DepartmentId)
        //    .HasConstraintName("ahmet");
        #endregion
        #region HasData
        //modelBuilder.Entity<Department>().HasData(
        //    new Department()
        //    {
        //        Name = "asd",
        //        Id = 1
        //    });
        //modelBuilder.Entity<Person>().HasData(
        //    new Person
        //    {
        //        Id = 1,
        //        DepartmentId = 1,
        //        Name = "ahmet",
        //        Surname = "filanca",
        //        Salary = 100,
        //        CreatedDate = DateTime.Now
        //    },
        //    new Person
        //    {
        //        Id = 2,
        //        DepartmentId = 1,
        //        Name = "mehmet",
        //        Surname = "filanca",
        //        Salary = 200,
        //        CreatedDate = DateTime.Now
        //    }
        //    );
        #endregion
        #region HasDiscriminator
        //modelBuilder.Entity<Entity>()
        //    .HasDiscriminator<int>("Ayirici")
        //    .HasValue<A>(1)
        //    .HasValue<B>(2)
        //    .HasValue<Entity>(3);

        #endregion
        #region HasField
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Name)
        //    .HasField(nameof(Person._name));
        #endregion
        #region HasNoKey
        //modelBuilder.Entity<Example>()
        //    .HasNoKey();
        #endregion
        #region HasIndex
        //modelBuilder.Entity<Person>()
        //    .HasIndex(p => new { p.Name, p.Surname });
        #endregion
        #region HasQueryFilter
        //modelBuilder.Entity<Person>()
        //    .HasQueryFilter(p => p.CreatedDate.Year == DateTime.Now.Year);
        #endregion
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
    }
}

public class Flight
{
    public int FlightID { get; set; }
    public int DepartureAirportId { get; set; }
    public int ArrivalAirportId { get; set; }
    public string Name { get; set; }
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }
}

public class Airport
{
    public int AirportID { get; set; }
    public string Name { get; set; }
    [InverseProperty(nameof(Flight.DepartureAirport))]
    public virtual ICollection<Flight> DepartingFlights { get; set; }

    [InverseProperty(nameof(Flight.ArrivalAirport))]
    public virtual ICollection<Flight> ArrivingFlights { get; set; }
}
#endregion

#region Generated Values
// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

ApplicationDbContext context = new();

#region Generated Value Nedir?
//EF Core'da üretilen değerlerle ilgili çeşitli modellerin ayrıntılarını yapılandırmamızı sağlayan bir konfigürasyondur.
#endregion

#region Default Values

//EF Core herhangi bir tablonun herhangi bir kolonuna yazılım tarafından bir değer gönderilmediği taktirde bu kolona hangi değerin(default value) üretilip yazdırılacağını belirleyen yapılanmalardır.

#region HasDefaultValue
//Static veri veriyor.
#endregion

#region HasDefaultValueSql
//SQL cümleciği
#endregion

#endregion

#region Computed Columns

#region HasComputedColumnSql
//Tablo içerisindeki kolonlar üzerinde yapılan aritmatik işlemler neticesinde üretilen kolondur.
#endregion

#endregion

#region Value Generation

#region Primary Keys
//Herhangi bir tablodaki satırları kimlik vari şekilde tanımlayan, tekil(unique) olan sütun veya sütunlardır.
#endregion

#region Identity
//Identity, yalnızca otomatik olarak artan bir sütundur. Bir sütun, PK olmaksızın identity olarak tanımlanabilir. Bir tablo içerisinde identity kolonu sadece tek bir tane olarak tanımlanabilir.
#endregion

//Bu iki özellik genellikle birlikte kullanılmaktadırlar. O yüzden EF Core PK olan bir kolonu otomatik olarak Identity olacak şekilde yapılandırmaktadır. Ancak böyle olması için bir gereklilik yoktur!

#region DatabaseGenerated

#region DatabaseGeneratedOption.None - ValueGeneratedNever
//Bir kolonda değer üretilmeyecekse eğer None ile işaretliyoruz.
//EF Core'un default olarak PK kolonlarda getirdiği Identity özelliğini kaldırmak istiyorsak eğer None'ı kullanabiliriz.
#endregion

#region DatabaseGeneratedOption.Identity - ValueGeneratedOnAdd
//Herhangi bir kolona Identity özelliğini vermemizi sağlayan bir konfigürasyondur.

#region Sayısal Türlerde
//Eğer ki Identity özelliği bir tabloda sayısal olan bir kolonda kullanılacaksa o durumda ilgili tablodaki pk olan kolondan özellikle/iradlei bir şekilde identity özelliğinin kaldırılması gerekmektedir.(None)
#endregion

#region Sayısal Olmayan Türlerde
//Eğer ki Identity özelliği bir tabloda sayısal olmaan bir kolonda kullaınacaksa o durumda ilgili talbodaki pk olan kolondan iradeli bir şekilde identity özelliğinin kaldırılmasına gerek yoktur. 
#endregion

#endregion

#region DatabaseGeneratedOption.Computed - ValueGeneratedOnAddOrUpdate
//EF Core üzerinde bir kolon Computed column ise ister Computed olarak belirleyebilirsiniz isterseniz de belirmeden kullanmaya devam edebilirsiniz.
#endregion

#endregion

#endregion

Person p = new()
{
    Name = "Gençay",
    Surname = "Yıldız",

    Premium = 10,
    TotalGain = 110
};
await context.Persons.AddAsync(p);
await context.SaveChangesAsync();

class Person
{
    //[DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Premium { get; set; }
    public int Salary { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int TotalGain { get; set; }
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PersonCode { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.Salary)
            //.HasDefaultValue(100);
            .HasDefaultValueSql("FLOOR(RAND() * 1000)");

        modelBuilder.Entity<Person>()
            .Property(p => p.TotalGain)
            .HasComputedColumnSql("([Salary] + [Premium]) * 10")
            .ValueGeneratedOnAddOrUpdate();

        modelBuilder.Entity<Person>()
            .Property(p => p.PersonId)
            .ValueGeneratedNever();

        modelBuilder.Entity<Person>()
            .Property(p => p.PersonCode)
            .HasDefaultValueSql("NEWID()");

        modelBuilder.Entity<Person>()
            .Property(p => p.PersonCode)
            .ValueGeneratedOnAdd();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
    }
}

#endregion

#region IEntityTypeConfiguration

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

Console.WriteLine();

#region OnModelCreating
//Genel anlamda veritabanı ile ilgili konfigürasyonel operasyonların dışında entityler üzeirnde konfigürasyonel çalışmalar yapmamızı sağlayan bir fonskiyodundur.
#endregion

#region IEntityTypeConfiguration<T> Arayüzü
//Entity bazlı yapılacak olan konfigürasyonları o entitye özel harici bir dosya üzerinde yapmamızı sağlayan bir arayüzdür.

//Harici bir dosyada konfigürasyonların yürütülmesi merkezi bir yapılandırma noktası oluşturmamıızı sağlamaktadır.
//Harici bir dosyada konfigüarsyonların yürültülmesi entity sayısının fazla olduğu senaryolarda yönetilebilirliği artturacak ve yapılandırma ile ilgili geliştiricinin yükünü azaltacaktır.
#endregion

#region ApplyConfiguration Metodu
//Bu metot harici konfigürasyonel sınıflarımızı EF Core'a bildirebilmek için kullandığımız bir metotdur.
#endregion

#region ApplyConfigurationsFromAssembly Metodu
//Uygulama bazında oluşturulan harici konfigürasyonel sınıfların her birini OnModelCreating metodunda ApplyCOnfiguration ile tek tek bildirmek yerine bu sınıfların bulunduğu Assembly'i bildirerek IEntityTypeConfiguration arayüzünden türeyen tüm sınıfları ilgili entitye karşılık konfigürasyonel değer olarak baz almasını tek kalemde gerçekleştirmemizi sağlayan bir metottur.
#endregion

class Order
{
    public int OrderId { get; set; }
    public string Description { get; set; }
    public DateTime OrderDate { get; set; }
}

class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.OrderId);
        builder.Property(p => p.Description)
            .HasMaxLength(13);
        builder.Property(p => p.OrderDate)
            .HasDefaultValueSql("GETDATE()");
    }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new OrderConfiguration());      
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!");
    }
}
#endregion

#region Seperated Configuration Classses

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

Console.WriteLine();

#region OnModelCreating
//Genel anlamda veritabanı ile ilgili konfigürasyonel operasyonların dışında entityler üzeirnde konfigürasyonel çalışmalar yapmamızı sağlayan bir fonskiyodundur.
#endregion

#region IEntityTypeConfiguration<T> Arayüzü
//Entity bazlı yapılacak olan konfigürasyonları o entitye özel harici bir dosya üzerinde yapmamızı sağlayan bir arayüzdür.

//Harici bir dosyada konfigürasyonların yürütülmesi merkezi bir yapılandırma noktası oluşturmamıızı sağlamaktadır.
//Harici bir dosyada konfigüarsyonların yürültülmesi entity sayısının fazla olduğu senaryolarda yönetilebilirliği artturacak ve yapılandırma ile ilgili geliştiricinin yükünü azaltacaktır.
#endregion

#region ApplyConfiguration Metodu
//Bu metot harici konfigürasyonel sınıflarımızı EF Core'a bildirebilmek için kullandığımız bir metotdur.
#endregion

#region ApplyConfigurationsFromAssembly Metodu
//Uygulama bazında oluşturulan harici konfigürasyonel sınıfların her birini OnModelCreating metodunda ApplyCOnfiguration ile tek tek bildirmek yerine bu sınıfların bulunduğu Assembly'i bildirerek IEntityTypeConfiguration arayüzünden türeyen tüm sınıfları ilgili entitye karşılık konfigürasyonel değer olarak baz almasını tek kalemde gerçekleştirmemizi sağlayan bir metottur.
#endregion

class Order
{
    public int OrderId { get; set; }
    public string Description { get; set; }
    public DateTime OrderDate { get; set; }
}

class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.OrderId);
        builder.Property(p => p.Description)
            .HasMaxLength(13);
        builder.Property(p => p.OrderDate)
            .HasDefaultValueSql("GETDATE()");
    }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new OrderConfiguration());      
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!");
    }
}

#endregion


#region Data Seeding


ApplicationDbContext context = new();

//Seed Data'lar migration'ların dışında eklenmesi ve değiştirilmesi beklenmeyen durumlar için kullanılan bir özelliktir!

#region Data Seeding Nedir?
//EF Core ile inşa edilen veritabanı içerisinde veritabanı nesneleri olabileceği gibi verilerinde migrate sürecinde üretilmesini isteyebiliriz.
//İşte bu ihtiyaca istinaden Seed Data özelliği ile EF Core üzerinden migrationlarda veriler oluşturabilir ve migrate ederken bu verileri hedef tablolarımıza basabiliriz.
//Seed Data'lar, migrate süreçlerinde hazır verileri tablolara basabilmek için bunları yazılım kısmında tutmamızı gerektirmektedirler. Böylece bu veriler üzerinde veritabanı seviyesainde istenilen manipülasyonlar gönül rahatlığıyla gerçekleştirilebilmektedir.

//Data Seeding özelliği şu noktalarda kullanılabilir;
//Test için geçici verilere ihtiyaç varsa,
//Asp.NET Core'daki Identity yapılanmasındaki roller gibi static değerler de tutulabilir.
//Yazılım için temel konfigürasyonel değerler.
#endregion
#region Seed Data Ekleme
//OnModelCreating metodu içerisinde Entity fonksiyonundan sonra çağrıulan HasData fonksiyonu ilgili entitye karşılık Seed Data'ları eklememizi sağlayan bir fonksiyondur.

//PK değerlerinin manuel olarak bildirilmesi/verilmesi gerekmektedir. Neden diye sorarsanız eğer, ilişkisel verileri de Seed Datalarla üretebilmek için...
#endregion
#region İlişkisel Tablolar İçin Seed Data Ekleme
//İlişkisel senaryolarda dependent table'a veri eklerken foreign key kolonunun propertysi varsa eğer ona ilişkisel değerini vererek ekleme işlemini yapıyoruz.
#endregion
#region Seed Datanın Primary Key'ini Değiştirme
//Eğer ki migrate edilen herhangi bir seed datanın sonrasında PK'i değiştirilirse bu datayla varsa ilişkisel başka veriler onlara cascade davranışı sergilenecektir.
#endregion



class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}
class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Post>()
            .HasData(
                new Post() { Id = 1, BlogId = 1, Title = "A", Content = "..." },
                new Post() { Id = 2, BlogId = 1, Title = "B", Content = "..." },
                new Post() { Id = 5, BlogId = 2, Title = "B", Content = "..." }
            );

        modelBuilder.Entity<Blog>()
            .HasData(
                new Blog() { Id = 11, Url = "www.gencayyildiz.com/blog" },
                new Blog() { Id = 2, Url = "www.bilmemne.com/blog" }
            );
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
    }
}
#endregion


#region Table Per Hierarchy 

ApplicationDbContext context = new();
#region Table Per Hierarchy (TPH) Nedir?
//Kalıtımsal ilişkiye sahip olan entitylerin olduğu senaryolarda her bir hiyerarşiye karşılık bir tablo oluşturan davranıştır.
#endregion
#region Neden Table Per Hierarchy Yaklaşımında Bir Tabloya İhtiyacımız Olsun?
//İÇerisinde benzer alanlara sahip olan entityleri migrate ettiğimizde her entitye karşılık bir tablo oluşturmaktansa bu entityleri tek bir tabloda modellemek isteyebilir ve bu tablodaki kayıtları discriminator kolonu üzerinden birbirlerinden ayırabiliriz. İşte bu tarz bir tablonun oluşturulması ve bu tarz bir tabloya göre sorgulama, veri ekleme, silme vs. gibi operasyonların şekillendirilmesi için TPH davranışını kullanabiliriz.
#endregion
#region TPH Nasıl Uygulanır?
//EF Core'da entity aransında temel bir kalıtımsal ilişki söz konusuysa eğer default oalrak kabul edilen davranıştır.
//O yüzden herhangi bir konfigüreasyon gerektirmez!
//Entityler kendi aralarında kalıtımsal ilişkiye sahip olmalı ve bu entitylerin hepsi DbContext nesnesine DbSet olarak eklenmelidir! 
#endregion
#region Discriminator Kolonu Nedir?
//Table Per Hierarchy yaklaşımı neticesinde kümülatif olarak inşa edilmiş tablonun hangi entitye karşılık veri tuttuğunu ayırt edebilmemizi sağlayan bir kolondur.
//EF Core tarafından otomatik olarak tabloya yerleştirilir.
//Default olarak içerisinde entity isimlerini tutar.
//Discriminator kolonunu komple özelleştirebiliriz.
#endregion
#region Discriminator Kolon Adı Nasıl Değiştirilir?
//Öncelikle hiyerarşinin başında hangi sınıf varsa onun Fluent API'da konfigürasyonuna gidilmeli.
//Ardından HasDiscriminator fonksiyonu ile özelleştirilmeli.
#endregion
#region Discriminator Değerleri Nasıl Değiştirilir?
//Yine hiyearşinin bşaındaki entity konfigürasyonlarına gelip, HasDiscriminator fonksiyonu ile özelleştirmede bulunarak ardından HasValue fonksiyonu ile hangi entitye karşılık hangi değerin girieceğini belirtilen türde ifade edebilirsiniz.
#endregion
#region TPH'da Veri Ekleme
//Davranışların hiçbirinde veri eklerken,silerken, güncellerken vs. normal operasyonların dışında bir işlem yapılmaz!
//Hangi davranışıo kullanıyorsanız EF Core ona göre arkaplanda modellemeyi gerçekleştirecektir.
//Employee e1 = new() { Name = "Gençay", Surname = "Yıldız", Department = "Yazılım Bilgi İşlem" };
//Employee e2 = new() { Name = "Nevin", Surname = "Yıldız", Department = "Yazılım Bilgi İşlem" };
//Customer c1 = new() { Name = "Ahmet", Surname = "Bilmemne", CompanyName = "Ahmet Bilmemne Halı Kilim Yıkama" };
//Customer c2 = new() { Name = "Şuayip", Surname = "XYZ", CompanyName = "Şuayip Sucuk" };
//Technician t1 = new() { Name = "Rıfkı", Surname = "Kıllıbacak", Department = "Muhasebe", Branch = "Şöför" };
//await context.Employees.AddAsync(e1);
//await context.Employees.AddAsync(e2);
//await context.Customers.AddAsync(c1);
//await context.Customers.AddAsync(c2);
//await context.Technicians.AddAsync(t1);

//await context.SaveChangesAsync();
#endregion
#region TPH'da Veri Silme
//TPH davranışında silme operasyonu yine entity üzerinden gerçekleştirilir.
//var employee = await context.Employees.FindAsync(1);
//context.Employees.Remove(employee);
//await context.SaveChangesAsync();

//var customers = await context.Customers.ToListAsync();
//context.Customers.RemoveRange(customers);
//await context.SaveChangesAsync();
#endregion
#region TPH'da Veri Güncelleme
//TPH davranışında güncelleme operasyonu yine entity üzerinden gerçekleştirilir.
//Employee guncellenecek = await context.Employees.FindAsync(8);
//guncellenecek.Name = "Hilmi";
//await context.SaveChangesAsync();
#endregion
#region TPH'da Veri Sorgulama
//Veri sorgulama oeprasyonu bilinen DbSet propertysi üzerinden sorgulamadır. Ancak burada dikkat edilmesi gereken bir husus vardır. O da şu;
//var employees = await context.Employees.ToListAsync();
//var techs = await context.Technicians.ToListAsync();
//kalıtımsal ilişkiye göre yapılan sorgulamada üst sınıf alt sınıftaki verileride kapsamaktadır. O yüzden üst sınıfların sorgulamalarında alt sınıfların verileride gelecektir buna dikkat edilmelidir.
//Sorgulama süreçlerinde EF Core generate edilen sorguya bir where şartı eklemektedir.
#endregion
#region Farklı Entity'ler de Aynı İsimde Sütunların Olduğu Durumlar
//Entitylerde mükerrer kolonlar olabilir. Bu kolonları EF core isimsel olarak özelleştirip ayıracaktır.
#endregion
abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
class Employee : Person
{
    public string? Department { get; set; }
}
class Customer : Person
{
    public int A { get; set; }
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
    public int A { get; set; }
    public string? Branch { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Person>()
        //    .HasDiscriminator<string>("ayirici")
        //    .HasValue<Person>("A")
        //    .HasValue<Employee>("B")
        //    .HasValue<Customer>("C")
        //    .HasValue<Technician>("D");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}
#endregion

#region Table Per Type Davranışı

ApplicationDbContext context = new();

#region Table Per Type (TPT) Nedir?
//Entitylerin aralarında kalıtımsal ilişkiye sahip olduğu durumlarda her bir türe/entitye/tip/referans karşılık bir tablo generate eden davranıştır.
//Her generate edilen bu tablolar hiyerarşik düzlemde kendi aralarında birebir ilişkiye sahiptir.
#endregion
#region TPT Nasıl Uygulanır?
//TPT'yi uygulayabilmek için öncelikle entitylerin kendi aralarında olması gereken mantıkta inşa edilmesi gerekmektedir.
//Entityler DbSet olarak bildirilmelidir.
//Hiyerarşik olarak aralarında kalıtımsal ilişki olan tüm entityler OnModelCreating fonksiyonunda ToTable metodu ile konfigüre edilmelidir. Böylece EF Core kalıtımsal ilişki olan bu tablolar arasında TPT davranışının olduğunu anlayacaktır.
#endregion
#region TPT'de Veri Ekleme
//Technician technician = new() { Name = "Şuayip", Surname = "Yıldız", Department = "Yazılım", Branch = "Kodlama" };
//await context.Technicians.AddAsync(technician);

//Customer customer = new() { Name = "Hilmi", Surname = "Celayir", CompanyName = "Çaykur" };
//await context.Customers.AddAsync(customer);
//await context.SaveChangesAsync();
#endregion
#region TPT'de Veri Silme
//Employee? silinecek = await context.Employees.FindAsync(3);
//context.Employees.Remove(silinecek);
//await context.SaveChangesAsync();

//Person? silinecekPerson = await context.Persons.FindAsync(1);
//context.Persons.Remove(silinecekPerson);
//await context.SaveChangesAsync();
#endregion
#region TPT'de Veri Güncelleme
//Technician technician = await context.Technicians.FindAsync(2);
//technician.Name = "Mehmet";
//await context.SaveChangesAsync();
#endregion
#region TPT'de Veri Sorgulama
//Employee employee = new() { Name = "Fatih", Surname = "Yavuz", Department = "ABC" };
//await context.Employees.AddAsync(employee);
//await context.SaveChangesAsync();

//var technicians = await context.Technicians.ToListAsync();
//var employees = await context.Employees.ToListAsync();

//Console.WriteLine();
#endregion



abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
class Employee : Person
{
    public string? Department { get; set; }
}
class Customer : Person
{
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
    public string? Branch { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("Persons");
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Technician>().ToTable("Technicians");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}
#endregion

#region Table Per Concrete Type


ApplicationDbContext context = new();

#region Table Per Concrete Type (TPC) Nedir?
//TPC davranışı, kalıtımsal ilişkiye sahip olan entitylerin olduğu çalışmalarda sadece concrete/somut olan entity'lere karşılık bir tablo oluşturacak davranış modelidir.
//TPC, TPT'nin daha performanslı versiyonudur.
#endregion
#region TPC Nasıl Uygulanır?
//Hiyerarşik düzlemde abstract olan yapılar üzerinden OnModelCreating üzerinden Entity fonskiyonuyla konfigürasyona girip ardından UseTpcMappingStrategy fonksiyonu eşliğinde davranışın TPC olacağını belirleyebiliriz.
#endregion
#region TPC'de Veri Ekleme
//await context.Technicians.AddAsync(new() { Name = "Gençay", Surname = "Yıldız", Branch = "Yazılım", Department = "Yazılım Departmanı" });
//await context.Technicians.AddAsync(new() { Name = "Mustafa", Surname = "Yıldız", Branch = "Yazılım", Department = "Yazılım Departmanı" });
//await context.Technicians.AddAsync(new() { Name = "Hilmi", Surname = "Yıldız", Branch = "Yazılım", Department = "Yazılım Departmanı" });
//await context.SaveChangesAsync();
#endregion
#region TPC'de Veri Silme
//Technician? silinecek = await context.Technicians.FindAsync(2);
//context.Technicians.Remove(silinecek);
//await context.SaveChangesAsync();
#endregion
#region TPC'de Veri Güncelleme
//Technician? guncellenecek = await context.Technicians.FindAsync(3);
//guncellenecek.Name = "Ahmet";
//await context.SaveChangesAsync();
#endregion
#region TPC'de Veri Sorgulama
//var datas = await context.Technicians.ToListAsync();
//Console.WriteLine();
#endregion
abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}
class Employee : Person
{
    public string? Department { get; set; }
}
class Customer : Person
{
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
    public string? Branch { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TPT
        //modelBuilder.Entity<Person>().ToTable("Persons");
        //modelBuilder.Entity<Employee>().ToTable("Employees");
        //modelBuilder.Entity<Customer>().ToTable("Customers");
        //modelBuilder.Entity<Technician>().ToTable("Technicians");
        modelBuilder.Entity<Person>().UseTpcMappingStrategy();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}

#endregion

#region Contstraints


ApplicationDbContext context = new();

#region Primary Key Constraint

//Bir kolonu PK constraint ile birincil anahtar yapmak istiyorsak eğer bunun için name convention'dan istifade edebiliriz. Id, ID, EntityNameId, EntityNameID şeklinde tanımlanan tüm propertyler default olarak EF Core tarafından pk constraint olacak şekilde generate edilirler.
//Eğer ki, farklı bir property'e PK özelliğini atamak istiyorsan burada HasKey Fluent API'ı yahut Key attribute'u ile bu bildirimi iradeli bir şekilde yapmak zorundasın.

#region HasKey Fonksiyonu

#endregion
#region Key Attribute'u

#endregion
#region Alternate Keys - HasAlternateKey
//Bir entity içerisinde PK'e ek olarak her entity instance'ı için alternatif bir benzersiz tanımlayıcı işlevine sahip olan bir key'dir.
#endregion
#region Composite Alternate Key

#endregion

#region HasName Fonksiyonu İle Primary Key Constraint'e İsim Verme

#endregion
#endregion

#region Foreign Key Constraint

#region HasForeignKey Fonksiyonu

#endregion
#region ForeignKey Attribute'u

#endregion
#region Composite Foreign Key

#endregion

#region Shadow Property Üzerinden Foreign Key

#endregion

#region HasConstraintName Fonksiyonu İle Primary Key Constraint'e İsim Verme

#endregion
#endregion

#region Unique Constraint

#region HasIndex - IsUnique Fonksiyonları

#endregion

#region Index, IsUnique Attribute'ları

#endregion

#region Alternate Key

#endregion
#endregion

#region Check Constratint

#region HasCheckConstraint

#endregion
#endregion

//[Index(nameof(Blog.Url), IsUnique = true)]
class Blog
{
    public int Id { get; set; }
    public string BlogName { get; set; }
    public string Url { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    //public int BlogId { get; set; }
    public string Title { get; set; }
    public string BlogUrl { get; set; }
    public int A { get; set; }
    public int B { get; set; }

    public Blog Blog { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Blog>()
        //    .HasKey(b => b.Id)
        //    .HasName("ornek");
        //modelBuilder.Entity<Blog>()
        //    .HasAlternateKey(b => new { b.Url, b.BlogName });
        //modelBuilder.Entity<Blog>()
        //    .Property<int>("BlogForeignKeyId");

        //modelBuilder.Entity<Blog>()
        //    .HasMany(b => b.Posts)
        //    .WithOne(b => b.Blog)
        //    .HasForeignKey("BlogForeignKeyId")
        //    .HasConstraintName("ornekforeignkey");

        //modelBuilder.Entity<Blog>()
        //    .HasIndex(b => b.Url)
        //    .IsUnique();
        //modelBuilder.Entity<Blog>()
        //    .HasAlternateKey(b => b.Url);

        modelBuilder.Entity<Post>()
            .HasCheckConstraint("a_b_check_const", "[A] > [B]");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}
#endregion

#region Indexes

ApplicationDbContext context = new();

#region Index Nedir?
//Index, bir sütuna dayalı sorgulamaları daha verimli ve performanslı hale getirmek için kullanılan yapıdır.
#endregion
#region Index'leme Nasıl Yapılır?
//PK, FK ve AK olan kolonlar otomatik olarak indexlenir. 

#region Index Attribute'u

#endregion
#region HasIndex Metodu

#endregion
#endregion
#region Composite Index
//context.Employees.Where(e => e.Name == "" || e.Surname == "")
#endregion
#region Birden Fazla Index Tanımlama

#endregion
#region Index Uniqueness

#endregion
#region Index Sort Order - Sıralama Düzeni (EF Core 7.0)

#region AllDescending - Attribute
//Tüm indexlemelerde descending davranışının bütünsel olarak konfigürasyonunu sağlar.
#endregion
#region IsDescending - Attribute
//Indexleme sürecindeki her bir kolona göre sıralama davranışını hususi ayarlamak istiyorsak kullanılır.
#endregion
#region IsDescending Metodu

#endregion
#endregion
#region Index Name

#endregion
#region Index Filter

#region HasFilter Metodu

#endregion
#endregion
#region Included Columns

#region IncludeProperties Metodu

#endregion
#endregion



//[Index(nameof(Name))]
//[Index(nameof(Surname))]
//[Index(nameof(Name), nameof(Surname))]
//[Index(nameof(Name), AllDescending = true)]
//[Index(nameof(Name), nameof(Surname), IsDescending = new[] { true, false })]
//[Index(nameof(Name), Name = "name_index")]
class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Employee>()
        //.HasIndex(x => x.Name);
        //.HasIndex(x => new { x.Name, x.Surname });
        //.HasIndex(nameof(Employee.Name), nameof(Employee.Surname));
        //.HasIndex(x => x.Name)
        //.IsUnique();

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => x.Name)
        //    .IsDescending();

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => new { x.Name, x.Surname })
        //    .IsDescending(true, false);

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => x.Name)
        //    .HasDatabaseName("name_index");

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => x.Name)
        //    .HasFilter("[NAME] IS NOT NULL");

        modelBuilder.Entity<Employee>()
            .HasIndex(x => new { x.Name, x.Surname })
            .IncludeProperties(x => x.Salary);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}

#endregion

#region Sequences
ApplicationDbContext context = new();

#region Sequence Nedir?
//Veritabanında benzersiz ve ardışık sayısal değerler üreten veritabanı nesnesidir.
//Sequence herhangi bir tablonun özelliği değildir. Veritabanı nesnesidir. Birden fazla tablo tarafından kullanılabilir.
#endregion
#region Sequence Tanımlama
//Sequence'ler üzerinden değer oluştururken veritabanına özgü çalışma yapılması zaruridir. SQL Server'a özel yazılan Sequence tanımı misal olarak Oracle için hata verebilir.

#region HasSequence

#endregion
#region HasDefaultValueSql

#endregion
#endregion

//await context.Employees.AddAsync(new() { Name = "Gençay", Surname = "Yıldız", Salary = 1000 });
//await context.Employees.AddAsync(new() { Name = "Mustafa", Surname = "Yıldız", Salary = 1000 });
//await context.Employees.AddAsync(new() { Name = "Tuaip", Surname = "Yıldız", Salary = 1000 });

//await context.Customers.AddAsync(new() { Name = "Muiddin" });
//await context.SaveChangesAsync();

#region Sequence Yapılandırması

#region StartsAt

#endregion
#region IncrementsBy

#endregion
#endregion
#region Sequence İle Identity Farkı
//Sequence bir veritabanı nesnesiyken, Identity ise tabloların özellikleridir.
//Yani Sequence herhangi bir tabloya bağımlı değildir. 
//Identity bir sonraki değeri diskten alırken Sequence ise RAM'den alır. Bu yüzden önemli ölçüde Identity'e nazaran daha hızlı, performanslı ve az maliyetlidir.
#endregion

class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
}
class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence("EC_Sequence")
            .StartsAt(100)
            .IncrementsBy(5);


        modelBuilder.Entity<Employee>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");

        modelBuilder.Entity<Customer>()
            .Property(c => c.Id)
            .HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}
#endregion

#region Explicit Loading

ApplicationDbContext context = new();
#region Explicit Loading

//Oluşturulan sorguya eklenecek verilerin şartlara bağlı bir şekilde/ihtiyaçlara istinaden yüklenmesini sağlayan bir yaklaşımdır.

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//if (employee.Name == "Gençay")
//{
//    var orders = await context.Orders.Where(o => o.EmployeeId == employee.Id).ToListAsync();
//}

#region Reference

//Explicit Loading sürecinde ilişkisel olarak sorguya eklenmek istenen tablonun navigation propertysi eğer ki tekil bir türse bu tabloyu reference ile sorguya ekleyebilemkteyiz.

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
////...
////...
////...
//await context.Entry(employee).Reference(e => e.Region).LoadAsync();

//Console.WriteLine();
#endregion
#region Collection

//Explicit Loading sürecinde ilişkisel olarak sorguya eklenmek istenen tablonun navigation propertysi eğer ki çoğul/koleksiyonel bir türse bu tabloyu Collection ile sorguya ekleyebilemkteyiz.

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//...
//...
//await context.Entry(employee).Collection(e => e.Orders).LoadAsync();

//Console.WriteLine();
#endregion

#region Collection'lar da Aggregate Operatör Uygulamak
//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//...
//...
//var count = await context.Entry(employee).Collection(e => e.Orders).Query().CountAsync();
Console.WriteLine();
#endregion
#region Collection'lar da Filtreleme Gerçekleştirmek
//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
////...
////...
////...
//var orders = await context.Entry(employee).Collection(e => e.Orders).Query().Where(q => q.OrderDate.Day == DateTime.Now.Day).ToListAsync();
#endregion
#endregion

public class Employee
{
    public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public List<Order> Orders { get; set; }
    public Region Region { get; set; }
}
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    public Employee Employee { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Region> Regions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}

#endregion

#region Lazy Loading


ApplicationDbContext context = new();

#region Lazy Loading Nedir?
//Navigation property'ler üzerinde bir işlem yapılmaya çalışıldığı taktirde ilgili propertynin/ye temsil ettiği/karşılık gelen tabloya özel bir sorgu oluşturulup execute edilmesini ve verilerin yüklenmesini sağlayan bir yaklaşımdır.
#endregion

//var employee = await context.Employees.FindAsync(2);
//Console.WriteLine(employee.Region.Name);

#region Prox'lerle Lazy Loading
//Microsoft.EntityFrameworkCore.Proxies

#region Property'lerin virtual Olması
//Eğer ki proxler üzerinden lazy loading operasyonu gerçekleştiriyorsanız Navigtation Propertylerin virtual ile işaretlenmiş olması gerekmektedir. Aksi taktirde patlama meydana gelecektir.
#endregion
#endregion

#region Proxy Olmaksızın Lazy Loading
//Prox'ler tüm platformlarda desteklenmeyebilir. Böyle bir durumda manuel bir şekilde lazy loading'i uygulamak mecburiyetinde kalabiliriz.

//Manuel yapılan Lazy Loading operasyonlarında Navigation Proeprtylerin virtual ile işaretlenmesine gerek yoktur!

#region ILazyLoader Interface'i İle Lazy Loading
//Microsoft.EntityFrameworkCore.Abstractions
//var employee = await context.Employees.FindAsync(2);
#endregion
#region Delegate İle Lazy Loading
//var employee = await context.Employees.FindAsync(2);
#endregion
#endregion

#region N+1 Problemi
//var region = await context.Regions.FindAsync(1);
//foreach (var employee in region.Employees)
//{
//    var orders = employee.Orders;
//    foreach (var order in orders)
//    {
//        Console.WriteLine(order.OrderDate);
//    }
//}
#endregion

//Lazy Loading, kullanım açısından oldukça maliyetli ve performans düşürücü bir etkiye sahip yöntemdir. O yüzden kullanırken mümkün mertebe dikkatli olmalı ve özellikle navigation propertylerin döngüsel tetiklenme durumlarında lazy loading'i tercih etmemeye odaklanmalıyız. Aksi taktirde her bir tetiklemeye karşılık aynı sorguları üretip execute edecektir. Bu durumu N+1 Problemi olarak nitelendirmekteyiz.
//Mümkün mertebe, ilişkisel verileri eklerken Lazy Loading kullanmamaya özen göstermeliyiz.

Console.WriteLine();

#region Proxy İle Lazy Loading
public class Employee
{
    public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
    public virtual List<Order> Orders { get; set; }
    public virtual Region Region { get; set; }
}
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual Employee Employee { get; set; }
}
#endregion
#region ILazyLoader Interface'i İle Lazy Loading
//public class Employee
//{
//    ILazyLoader _lazyLoader;
//    Region _region;
//    public Employee() { }
//    public Employee(ILazyLoader lazyLoader)
//        => _lazyLoader = lazyLoader;
//    public int Id { get; set; }
//    public int RegionId { get; set; }
//    public string? Name { get; set; }
//    public string? Surname { get; set; }
//    public int Salary { get; set; }
//    public List<Order> Orders { get; set; }
//    public Region Region
//    {
//        get => _lazyLoader.Load(this, ref _region);
//        set => _region = value;
//    }
//}
//public class Region
//{
//    ILazyLoader _lazyLoader;
//    ICollection<Employee> _employees;
//    public Region() { }
//    public Region(ILazyLoader lazyLoader)
//        => _lazyLoader = lazyLoader;
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Employee> Employees
//    {
//        get => _lazyLoader.Load(this, ref _employees);
//        set => _employees = value;
//    }
//}
//public class Order
//{
//    public int Id { get; set; }
//    public int EmployeeId { get; set; }
//    public DateTime OrderDate { get; set; }
//    public Employee Employee { get; set; }
//}

#endregion
#region Delegate İle Lazy Loading
//public class Employee
//{
//    Action<object, string> _lazyLoader;
//    Region _region;
//    public Employee() { }
//    public Employee(Action<object, string> lazyLoader)
//        => _lazyLoader = lazyLoader;
//    public int Id { get; set; }
//    public int RegionId { get; set; }
//    public string? Name { get; set; }
//    public string? Surname { get; set; }
//    public int Salary { get; set; }
//    public List<Order> Orders { get; set; }
//    public Region Region
//    {
//        get => _lazyLoader.Load(this, ref _region);
//        set => _region = value;
//    }
//}
//public class Region
//{
//    Action<object, string> _lazyLoader;
//    ICollection<Employee> _employees;
//    public Region() { }
//    public Region(Action<object, string> lazyLoader)
//        => _lazyLoader = lazyLoader;
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Employee> Employees
//    {
//        get => _lazyLoader.Load(this, ref _employees);
//        set => _employees = value;
//    }
//}
//public class Order
//{
//    public int Id { get; set; }
//    public int EmployeeId { get; set; }
//    public DateTime OrderDate { get; set; }
//    public Employee Employee { get; set; }
//}

//static class LazyLoadingExtension
//{
//    public static TRelated Load<TRelated>(this Action<object, string> loader, object entity, ref TRelated navigation, [CallerMemberName] string navigationName = null)
//    {
//        loader.Invoke(entity, navigationName);
//        return navigation;
//    }
//}
#endregion

class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Region> Regions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");

        //optionsBuilder.UseLazyLoadingProxies();
    }
}
#endregion


#region Eager Loading

ApplicationDbContext context = new();
#region Loading Related Data

#region Eager Loading
//Eager loading, generate edilen bir sorguya ilişkisel verilerin parça parça eklenmesini sağlayan ve bunu yaparken iradeli/istekli bir şekilde yapmamızı sağlayan bir yöntemdir.

#region Include
//Eager loading operasyonunu yapmamızı sağlayan bir fonksiyondur.
//Yani üretilen bir sorguya diğer ilişkisel tabloların dahil edilmesini sağlayan bir işleve sahiptir..

//var employees = await context.Employees.Include("Orders").ToListAsync();
//var employees = await context.Employees
//    .Include(e => e.Region)
//    .Where(e => e.Orders.Count > 2)
//    .Include(e => e.Orders)
//    .ToListAsync();

#endregion
#region ThenInclude
//ThenInclude, üretilen sorguda Include edilen tabloların ilişkili olduğu diğer tablolarıda sorguya ekleyebilmek için kullanılan bir fonksiyondur. 
//Eğer ki, üretilen sorguya include edilen navigatiobn property koleksionel bir propertyse işte o zaman bu property üzerinden diğer ilişkisel tabloya erişim gösterilememektedir. Böyle bir durumda koleksiyonel propertylerin türlerine erişip, o tür ile ilişkili diğer tablolarıda sorguya eklememizi sağlayan fonksiyondur.

//var orders = await context.Orders
//    //.Include(o => o.Employee)
//    .Include(o => o.Employee.Region)
//    .ToListAsync();

//var regions = await context.Regions
//    .Include(r => r.Employees)
//        .ThenInclude(e => e.Orders)
//    .ToListAsync();

#endregion
#region Filtered Include
//Sorgulama süreçlerinde Include yaparken sonuçlar üzerinde filtreleme ve sıralama gerçekleştirebilmemiz isağlayan bir özleliktir.

//var regions = await context.Regions
//    .Include(r => r.Employees.Where(e => e.Name.Contains("a")).OrderByDescending(e => e.Surname))
//    .ToListAsync();

//Desktelenen fonksiyon : Where, OrderBy, OrderByDescending, ThenBy, ThenByDescending, Skip, Take

//Change Tracker'ın aktif olduğu durumlarda Include ewdilmiş sorgular üzerindeki filtreleme sonuçları beklenmeyen olabilir. Bu durum, daha önce sorgulanmş ve Change Tracker tarafından takip edilmiş veriler arasında filtrenin gereksinimi dışında kalan veriler için söz konusu olacaktır. Bundan dolayı sağlıklı bir filtred include operasyonu için change tracker'ın kullanılmadığı sorguları tercih etmeyi düşünebilirsiniz.

#endregion
#region Eager Loading İçin Kritik Bir Bilgi
//EF Core, önceden üretilmiş ve execute edilerek verileri belleğe alınmış olan sorguların verileri, sonraki sorgularda KULLANIR!

//var orders = await context.Orders.ToListAsync();

//var employees = await context.Employees.ToListAsync();

#endregion
#region AutoInclude - EF Core 6
//Uygulama seviyesinde bir entitye karşılık yapılan tüm sorgulamalarda "kesinlikle" bir tabloya Include işlemi gerçekleştirlecekse eğer bunu her bir sorgu için tek tek yapmaktansa merkezi bir hale getirmemizi sağlayan özelliktir.

//var employees = await context.Employees.ToListAsync();
#endregion
#region IgnoreAutoIncludes
//AutoInclude konfigürasyonunu sorgu seviyesinde pasifize edebilmek için kullandığımız fonksiyondur.

//var employees = await context.Employees.IgnoreAutoIncludes().ToListAsync();
#endregion
#region Birbirlerinden Türetilmiş Entity'ler Arasında Include

#region Cast Operatörü İle Include
var persons1 = await context.Persons.Include(p => ((Employee)p).Orders).ToListAsync();
#endregion
#region as Operatörü İle Include
var persons2 = await context.Persons.Include(p => (p as Employee).Orders).ToListAsync();
#endregion
#region 2. Overload İle Include
var persons3 = await context.Persons.Include("Orders").ToListAsync();
#endregion
#endregion


Console.WriteLine();
#endregion


#region Explicit Loading

#region Collection Fonksiyonu

#endregion
#region Reference Fonksiyonu

#endregion
#endregion

#region Lazy Loading

#region N + 1 Problemi

#endregion
#endregion
#endregion



public class Person
{
    public int Id { get; set; }

}
public class Employee : Person
{
    //public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public List<Order> Orders { get; set; }
    public Region Region { get; set; }
}
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    public Employee Employee { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Region> Regions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Employee>()
            .Navigation(e => e.Region)
            .AutoInclude();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}

#endregion

#region Complex Queries


ApplicationDbContext context = new();

#region Complext Query Operators

#region Join

#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//                on photo.PersonId equals person.PersonId
//            select new
//            {
//                person.Name,
//                photo.Url
//            };
//var datas = await query.ToListAsync();
#endregion
#region Method Syntax
//var query = context.Photos
//    .Join(context.Persons,
//    photo => photo.PersonId,
//    person => person.PersonId,
//    (photo, person) => new
//    {
//        person.Name,
//        photo.Url
//    });

//var datas = await query.ToListAsync();
#endregion

#region Multiple Columns Join

#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//                on new { photo.PersonId, photo.Url } equals new { person.PersonId, Url = person.Name }
//            select new
//            {
//                person.Name,
//                photo.Url
//            };
//var datas = await query.ToListAsync();
#endregion
#region Method Syntax
//var query = context.Photos
//    .Join(context.Persons,
//    photo => new
//    {
//        photo.PersonId,
//        photo.Url
//    },
//    person => new
//    {
//        person.PersonId,
//        Url = person.Name
//    },
//    (photo, person) => new
//    {
//        person.Name,
//        photo.Url
//    });

//var datas = await query.ToListAsync();
#endregion
#endregion

#region 2'den Fazla Tabloyla Join

#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//                on photo.PersonId equals person.PersonId
//            join order in context.Orders
//                on person.PersonId equals order.PersonId
//            select new
//            {
//                person.Name,
//                photo.Url,
//                order.Description
//            };

//var datas = await query.ToListAsync();
#endregion
#region Method Syntax
//var query = context.Photos
//    .Join(context.Persons,
//    photo => photo.PersonId,
//    person => person.PersonId,
//    (photo, person) => new
//    {
//        person.PersonId,
//        person.Name,
//        photo.Url
//    })
//    .Join(context.Orders,
//    personPhotos => personPhotos.PersonId,
//    order => order.PersonId,
//    (personPhotos, order) => new
//    {
//        personPhotos.Name,
//        personPhotos.Url,
//        order.Description
//    });

//var datas = await query.ToListAsync();
#endregion
#endregion

#region Group Join - GroupBy Değil!
//var query = from person in context.Persons
//            join order in context.Orders
//                on person.PersonId equals order.PersonId into personOrders
//            //from order in personOrders
//            select new
//            {
//                person.Name,
//                Count = personOrders.Count(),
//                personOrders
//            };
//var datas = await query.ToListAsync();
#endregion
#endregion

//DefaultIfEmpty : Sorgulama sürecinde ilişkisel olarak karşılığı olmayan verilere default değerini yazdıran yani LEFT JOIN sorgusunu oluşturtan bir fonksiyondur.

#region Left Join
//var query = from person in context.Persons
//            join order in context.Orders
//                on person.PersonId equals order.PersonId into personOrders
//            from order in personOrders.DefaultIfEmpty()
//            select new
//            {
//                person.Name,
//                order.Description
//            };

//var datas = await query.ToListAsync();
#endregion

#region Right Join
//var query = from order in context.Orders
//            join person in context.Persons
//                on order.PersonId equals person.PersonId into orderPersons
//            from person in orderPersons.DefaultIfEmpty()
//            select new
//            {
//                person.Name,
//                order.Description
//            };

//var datas = await query.ToListAsync();
#endregion

#region Full Join
//var leftQuery = from person in context.Persons
//                join order in context.Orders
//                    on person.PersonId equals order.PersonId into personOrders
//                from order in personOrders.DefaultIfEmpty()
//                select new
//                {
//                    person.Name,
//                    order.Description
//                };


//var rightQuery = from order in context.Orders
//                 join person in context.Persons
//                     on order.PersonId equals person.PersonId into orderPersons
//                 from person in orderPersons.DefaultIfEmpty()
//                 select new
//                 {
//                     person.Name,
//                     order.Description
//                 };

//var fullJoin = leftQuery.Union(rightQuery);

//var datas = await fullJoin.ToListAsync();
#endregion

#region Cross Join
//var query = from order in context.Orders
//            from person in context.Persons
//            select new
//            {
//                order,
//                person
//            };

//var datas = await query.ToListAsync();
#endregion

#region Collection Selector'da Where Kullanma Durumu
//var query = from order in context.Orders
//            from person in context.Persons.Where(p => p.PersonId == order.PersonId)
//            select new
//            {
//                order,
//                person
//            };

//var datas = await query.ToListAsync();
#endregion

#region Cross Apply
//Inner Join

//var query = from person in context.Persons
//            from order in context.Orders.Select(o => person.Name)
//            select new
//            {
//                person,
//                order
//            };

//var datas = await query.ToListAsync();
#endregion

#region Outer Apply
//Left Join
//var query = from person in context.Persons
//            from order in context.Orders.Select(o => person.Name).DefaultIfEmpty()
//            select new
//            {
//                person,
//                order
//            };

//var datas = await query.ToListAsync();
#endregion
#endregion
Console.WriteLine();
public class Photo
{
    public int PersonId { get; set; }
    public string Url { get; set; }

    public Person Person { get; set; }
}
public enum Gender { Man, Woman }
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }

    public Photo Photo { get; set; }
    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Photo>()
            .HasKey(p => p.PersonId);

        modelBuilder.Entity<Person>()
            .HasOne(p => p.Photo)
            .WithOne(p => p.Person)
            .HasForeignKey<Photo>(p => p.PersonId);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}
#endregion 

#region SQL Queryler



ApplicationDbContext context = new();

//Eğer ki, sorgunuzu LINQ ile ifade edemiyorsanız yahut lINQ'in ürettiği sorguya nazaran daha optimize bir sorguyu manuel geliştirmek ve EF Core üzerinden execute etmek istiyorsanız EF Core'un bu davrnaışı desteklediğini bilmelisiniz.

//Manuel bir şekilde/tarafımızca oluşturulmuş olan sorguları EF Core tarafından execute edebilmek için o sorgunun sonucunu karşılayacak bir entity model'ın tasarlanmış ve bunun DbSet olarak context nesnesine tanımlanmış olması gerekiyor.
#region FromSqlInterpolated
//EF Core 7.0 sürümünden önce ham sorguları execute edebildiğimiz fonksiyondur.
//var persons = await context.Persons.FromSqlInterpolated($"SELECT * FROM Persons")
//    .ToListAsync();
#endregion

#region FromSql - EF Core 7.0
//EF Core 7.0 ile gelen metottur. 

#region Query Execute
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons")
//.ToListAsync();
#endregion
#region Stored Procedure Execute
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons 4")
//    .ToListAsync();
#endregion
#region Parametreli Sorgu Oluşturma
#region Örnek 1
//int personId = 3;
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where PersonId = {personId}")
//    .ToListAsync();

//Burada sorguya geçirilen personId değişkeni arkaplanda bir DbParameter türüne dönüştürülerek o şekilde sorguya dahil edilmektedir.
#endregion
#region Örnek 2
//int personId = 3;
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons {personId}")
//    .ToListAsync();
#endregion
#region Örnek 3
//SqlParameter personId = new("PersonId", "3");
//personId.DbType = System.Data.DbType.Int32;
//personId.Direction = System.Data.ParameterDirection.Input;

//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where PersonId = {personId}")
//    .ToListAsync();
#endregion
#region Örnek 4
//SqlParameter personId = new("PersonId", "3");
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons {personId}")
//    .ToListAsync();
#endregion
#region Örnek 5
//SqlParameter personId = new("PersonId", "3");
//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons @PersonId = {personId}")
//    .ToListAsync();
#endregion
#endregion
#endregion

#region Dynamic SQL Oluşturma ve Parametre Girme - FromSqlRaw
//string columnName = "PersonId", value = "3";
//var persons = await context.Persons.FromSql($"Select * From Persons Where {columnName} = {value}")
//    .ToListAsync();

//EF Core dinamik olarak oluşturulan sorgularda özellikle kolon isimleri parametreleştirilmişse o sorguyu ÇALIŞTIRMAYACAKTIR!

//string columnName = "PersonId";
//SqlParameter value = new("PersonId", "3");
//var persons = await context.Persons.FromSqlRaw($"Select * From Persons Where {columnName} = @PersonId", value)
//    .ToListAsync();

//FromSql ve FromSqlInterpolated metotlarında SQL Injection vs. gibi güvenlik önlemleri alınmış vaziyettedir. Lakin dinamik olarak sorguları oluşturuyorsanız eğer burada güvenlik geliştirici sorumludur. Yani gelen sorguda/veri yorumlar, noktalı virgüller yahut SQL'e özel karakterlerin algılanması ve bunların temizlenmesi geliştirici tarafından gerekmektedir.
#endregion
#region SqlQuery - Entiy Olmayan Scalar Sorguların Çalıştırılması - Non Entity - EF Core 7.0
//Entity'si olmayan scalar sorguların çalıştırılıp sonucunu elde etmemizi sağlayan yeni bir fonksiyondur.
//var data = await context.Database.SqlQuery<int>($"SELECT PersonId FROM Persons")
//    .ToListAsync();

//var persons = await context.Persons.FromSql($"SELECT * FROM Persons")
//    .Where(p => p.Name.Contains("a"))
//    .ToListAsync();

//var data = await context.Database.SqlQuery<int>($"SELECT PersonId Value FROM Persons")
//    .Where(x => x > 5)
//    .ToListAsync();

//SqlQuery'de LINQ operatörleriyle sorguya ekstradan katkıda bulunmak istiyorsanız eğer bu sorgu neticesinde gelecek olan kolonun adını Value olarak bildirmeniz gerekmektedir. Çünkü, SqlQuery metodu sorguyu bir subquery olarak generate etmektedir. Haliyle bu durumdan dolayı LINQ ile verilen şart ifadeleri statik olarka Value kolonuna göre tasarlanmıştır. O yüzden bu şekilde bir çalışma zorunlu gerekmektedir.

#endregion
#region ExecuteSql
//Insert, update, delete
//await context.Database.ExecuteSqlAsync($"Update Persons SET Name = 'Fatma' WHERE PersonId = 1");
#endregion
#region Sınırlılıklar
//Queryler entity türünün tüm özellikleri için kolonlarda değer döndürmelidir.
//var persons = await context.Persons.FromSql($"SELECT Name, PersonId FROM Persons")
//    .ToListAsync();

//Sütun isimleri proıperty isimleriyle aynı olmalıdır.

//SQL Sorgusu Join yapısı İÇEREMEZ!!! Haliyle bu tarz ihtiyaç noktalarında Include fonksiyonu KULLANILMALIDIR!
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons")
//    .Include(p => p.Orders)
//    .ToListAsync();
#endregion
Console.WriteLine();
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}


//CREATE PROC sp_GetAllPersons
//(
//	@PersonId INT NULL
//)AS
//BEGIN
//	IF @PersonId IS NULL
//		SELECT * FROM Persons
//	ELSE
//		SELECT * FROM Persons WHERE PersonId = @PersonId
//END
#endregion

#region Viewlar 


ApplicationDbContext context = new();
#region View Nedir?
//Oluşturduğumuz kompleks sorguları ihtiyaç durumlarında daha rahat bir şekilde kullanabilmek için basitleştiren bir veritabanı objesidir.
#endregion
#region EF Core İle View Kullanımı

#region View Oluşturma
//1. adım : boş bir migration oluşturulmalıdır.
//2. adım : migration içerisindeki Up fonksiyonunda view'in create komutları, down fonksiyonunda ise drop komutları yazılmalıdır.
//3. adım : migrate ediniz.
#endregion
#region View'i DbSet Olarak Ayarlama
//View'i EF Core üzerinden sorgulayabilmek için view neticesini karşılayabilecek bir entity olşturulması ve bu entity türünden DbSet property'sinin eklenmesi gerekmektedir.
#endregion
#region DbSet'in Bir View Olduğunu Bildirmek

#endregion

//var personOrders = await context.PersonOrders
//    .Where(po => po.Count > 10)
//    .ToListAsync();

#region EF Core'da View'lerin Özellikleri
//Viewlerde primary key olmaz! Bu yüzden ilgili DbSet'in HasNoKey ile işaretlenmesi gerekemktedir.
//View neticesinde gelen veriler Change Tracker ile takip edilmezler. Haliyle üzerlerinde yapılan değişiklikleri EF Core veritabanına yansıtmaz

//var personOrder = await context.PersonOrders.FirstAsync();
//personOrder.Name = "Abuzer";
//await context.SaveChangesAsync();
#endregion
Console.WriteLine();
#endregion
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}
public class PersonOrder
{
    public string Name { get; set; }
    public int Count { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrder> PersonOrders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<PersonOrder>()
            .ToView("vm_PersonOrders")
            .HasNoKey();

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}
#endregion

#region Stored Procedures

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

ApplicationDbContext context = new();
#region Stored Procedure Nedir?
//SP, view'ler gibi kompleks sorgularımızı daha basit bir şekilde tekrar kullanılabilir  bir hale getirmemiz isağlayan veritabanı nesnesidir. 
//View'ler tablo misali bir davranış sergilerken, SP'lar ise fonksiyonel bir davranış sergilerler.
//Ve türlü türlü artılarıda vardır.
#endregion

#region EF Core İle Stored Procedure Kullanımı
#region Stored Procedure Oluşturma
//1. adım : boş bir migration oluşturunuz.
//2. adım : migration'ın içerisindeki Up fonksiyonuna SP'ın Create komutlarını yazınız, Down fonk. ise Drop komutlarını yazınız.
//3. adım : migrate ediniz.
#endregion
#region Stored Procedure'ü Kullanma
//SP'ı kullanabilmek için bir entity'e ihtiyacımız vardır. Bu entity'nin DbSet propertysi ollarak context nesnesine de eklenmesi gerekmektedir.
//Bu DbSet properytysi üzerinden FromSql fonksiyonunu kullanarak 'Exec ....' komutu eşliğinde SP yapılanmasını çalıştırıp sorgu neticesini elde edebilirsiniz.
#region FromSql
//var datas = await context.PersonOrders.FromSql($"EXEC sp_PersonOrders")
//    .ToListAsync();
#endregion
#endregion
#region Geriye Değer Döndüren Stored Procedure'ü Kullanma
//SqlParameter countParameter = new()
//{
//    ParameterName = "count",
//    SqlDbType = System.Data.SqlDbType.Int,
//    Direction = System.Data.ParameterDirection.Output
//};
//await context.Database.ExecuteSqlRawAsync($"EXEC @count = sp_bestSellingStaff", countParameter);
//Console.WriteLine(countParameter.Value);
#endregion
#region Parametreli Stored Procedure Kullanımı
#region Input Parametreli Stored Procedure'ü Kullanma

#endregion
#region Output Parametreli Stored Procedure'ü Kullanma

#endregion

//SqlParameter nameParameter = new()
//{
//    ParameterName = "name",
//    SqlDbType = System.Data.SqlDbType.NVarChar,
//    Direction = System.Data.ParameterDirection.Output,
//    Size = 1000
//};
//await context.Database.ExecuteSqlRawAsync($"EXECUTE sp_PersonOrders2 7, @name OUTPUT", nameParameter);
//Console.WriteLine(nameParameter.Value);

#endregion
#endregion
Console.WriteLine();
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}
[NotMapped]
public class PersonOrder
{
    public string Name { get; set; }
    public int Count { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrder> PersonOrders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<PersonOrder>()
            .HasNoKey();

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
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


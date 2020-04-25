## EF core串聯刪除 

[串聯刪除] (https://docs.microsoft.com/zh-tw/ef/core/saving/cascade-delete)

[tutorial] (https://entityframeworkcore.com/saving-data-cascade-delete)

##說明

* StudentClassRelation (關聯檔)
* Class
* Student

``` js
 var relation = context.StudentClassRelations
                    .Include(c => c.Classes)
                    .Include(c => c.Students)
                    .FirstOrDefault();

```

對StudentClassRelations刪除的話只會刪到本身，跟他有關聯的檔並不會刪除
所以這邊做Include其實是無效的

``` js
 var relation1 = context.Classes
                    .Include(c => c.StudentClassRelations)
                    .ThenInclude(c => c.Students)
                    .FirstOrDefault();

```
對其中一個主檔做刪除，雖然這邊有Include到第三層Students的物件，但是只會刪除到關連檔而已，
除此之外DbContext的關連檔也要設定成.OnDelete(DeleteBehavior.Cascade)，如果用預設的會出現Exception


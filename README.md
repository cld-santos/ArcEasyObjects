# ArcEasy || ArcEasyObjects #

Could be awesome to write ArcObjects code like bellow?

```
#!c#

    PontoNotavel _pn = new PontoNotavel(_workspace);

    _pn.Codigo = 1;
    _pn.Descricao = "Test the insert of a Model.";
    _pn.Nome = "Insert Test.";

    _pn.Save();

```
```
#!c#

    PontoNotavel _pn = new PontoNotavel(_workspace);

    var _pns = _pn.Search("PontoNotavel.Codigo = 1 and PontoNotavel inside (Municipio.codigo=1)");
            
    foreach (PontoNotavel _item in _pns)
    {
        Assert.AreEqual(_item.Nome, "Teste Inclusao.");
    }
                       
    Assert.IsTrue(_pns.Count > 0);
```


Pleasure to meet you, I'm ArcEasyObjects!

ArcEasy is an ORM (Object Relation Mapping) proposal to make easy the life of an ArcObjects Developer.

Once you need to add a new Developer to your GIS Team, you need to find out an ESRI Developer, so you discover that is more easy to found a Microsoft Developer than an ArcObjects Developer.

To facilate the introduce of a MS Developer to ArcObjects world, and increase the productivity of your team try ArcEasy in your Project.


Make contact to help us to develop this amazing tool. 

###  Disclaimer ###

Don't you think that don't need your Developer know ArcObjects?
But we know that it's not the right thought, since the creation of Hibernate don't eliminate the need to know sql, unless you need lazy (in the bad meaning) developer.

### Comitters ###

* Claudio Santos -  [http://simplologia.com.br/](Simplologia)
* Daniel Porfirio - [http://danielporfirio.com/](danielporfirio.com)
# ArcEasy || ArcEasyObjects #

Could be awesome to write ArcObjects code like bellow?

```
#!c#

    PontoNotavel _pn = new PontoNotavel(new FeatureClassDAO(_workspace));

    _pn.Codigo = 1;
    _pn.Descricao = "Testando a inclusao por uma camada transparente.";
    _pn.Nome = "Teste Inclusao.";

    _pn.Save();

```
Pleasure to meet you, I'm ArcEasyObjects!

ArcEasy is an ORM (Object Relation Mapping) proposal to make easy the life of an ArcObjects Developer.

Once you need to add a new Developer to your GIS Team, you need to find out an ESRI Developer, so you discover that is more easy to found a Microsft Developer than an ArcObjects Developer.

###  Disclaimer ###

Don't you think that don't need your Developer know ArcObjects?
But we know that it's not the right thought, since the creation of Hibernate don't eliminate the need to know sql, unless you need lazy (in the bad meaning) developer.

### Comitters ###

* Claudio Santos -  [http://simplologia.com.br/](Simplologia)
* Daniel Porfirio - [http://danielporfirio.com/](danielporfirio.com)
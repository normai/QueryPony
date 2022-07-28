# QueryPony

**Slogan** : A Simple Generic Database Query Tool and API

**Places** : [Homepage](http://downtown.trilo.de/svn/queryponydev/trunk/home/index.html)
 — [Downloads](http://downtown.trilo.de/svn/queryponydev/downloads/)
 — [UserGuide](http://downtown.trilo.de/svn/queryponydev/trunk/QueryPony/QueryPonyGui/docs/index.html)

**Status** : Under overhaul — Disrupted

**Highlight** : Database agnostic API utilizable from any .NET program

## Quickstart

[Download](http://downtown.trilo.de/svn/queryponydev/downloads/) the executable and start it.

Select the 'SQLite' tab. Press 'Local Demo Connection'. Nod the turning up dialog.

Now the entries for a demo database 'joespostbox.201307031243.sqlite3'
 should appear.

![QuerPony started](./QueryPonyGui/docs/img/20180819o0212.querypony-started.v0.png)

Press the 'Connect' button and beeee patient (e.g. 30 seconds).

If the connection is established, it will be shown as a new node in the
 left side tree view.

Expand the tree and rightclick a table.

![Rightclick a table](./QueryPonyGui/docs/img/20180819o0213.querypony-select.v1.png)

Select the top item of the turned up context menu.

Now in the 'Statements' pane, a SQL select statement shall be written.

![Press execute button](./QueryPonyGui/docs/img/20180819o0214.querypony-execute.v1.png)

Press the 'Execute' toolbar button (the green triangle) or F5.

The statement will be executed, and the result will be shown in the
 ResultSet pane.

![View the result](./QueryPonyGui/docs/img/20180819o0215.querypony-result.v0.png)

View the result of your SQL statement.

To edit the table, write/mark/execute an appropriate SQL update
 statement. E.g.:

```
 UPDATE addresses SET name1 = "Mister" WHERE name2 = "Joe Jackson";
```

![View the result](./QueryPonyGui/docs/img/20180819o0216.querypony-helpmenu.v0.png)

Sure you also want inspect the meager offline User Guide
 which will show up in your default browser
 (here [online](https://rawgit.com/normai/QueryPony/master/QueryPonyGui/docs/index.html)).

![View the result](./QueryPonyGui/docs/img/20180819o0217.querypony-userguide.v0.png)

Holla, where do those pages come from? How can the browser show
 HTML files which did not even exist?

To resolve this riddle, seek some hint from the 'About' dialog.
 Open the User Settings folder, and you will see.

![View the result](./QueryPonyGui/docs/img/20180819o0218.querypony-aboutbox.v0.png)

Now you also have found the two folders, which you must delete, once you get
 tired of QueryPony and want purge it from your machine without any residues.

Have fun!

*2018-Aug-19 ~~*

---

<sup><sub><sup>File 20180819°0331, solution 20130603°1234 ܀Ω</sup></sub></sup>

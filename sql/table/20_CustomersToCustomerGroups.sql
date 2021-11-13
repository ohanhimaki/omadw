insert into dw.customerCustomerGroups
SELECT c.id, 1, 100 --ruokakauppa
from dw.customer c
         left join dw.customerCustomerGroups cgg on c.Id = cgg.customerid
where (name like '%market%'
    OR name like '%halli%'
    OR name like '%7-eleven%'
    OR name like '%halpa%'
    OR name like '%sale%'
    OR name like '%cafe mondeo%'
    )
  and cgg.customerid is null
    insert into dw.customerCustomerGroups
SELECT c.id, 2, 100 --pizza
from dw.customer c
         left join dw.customerCustomerGroups cgg on c.Id = cgg.customerid
where (name like '%pizz%'
    OR name like '%piikki%'
    OR name like '%sherifi keb%'
    OR name like '%foodora%'
    OR name like '%valentina%'
    OR name like '%rosso%'
    OR name like '%buffa %'
    OR name like '%sofra%'
    OR name like '%meram%'
    OR name like '%golani%'
    OR name like '%kebab%'
    )
  and cgg.customerid is null
    insert into dw.customerCustomerGroups
SELECT c.id, 3, 100 --urheilu
from dw.customer c
         left join dw.customerCustomerGroups cgg on c.Id = cgg.customerid
where (name like '%xxl%'
    OR name like '%cycli%'
    OR name like '%hirsimaki%'
    OR name like '%sofram%'
    OR name like '%jarno my%'
    )
  and cgg.customerid is null

    insert into dw.customerCustomerGroups
SELECT c.id, 4, 100 --asumiskulut
from dw.customer c
         left join dw.customerCustomerGroups cgg on c.Id = cgg.customerid
where (name like '%hannu paavola%'
    OR name like '%laura riihi%'
    )
  and cgg.customerid is null

    insert into dw.customerCustomerGroups
SELECT c.id, 5, 100 --lounasraviintolat
from dw.customer c
         left join dw.customerCustomerGroups cgg on c.Id = cgg.customerid
where (name like '%mangal%'
    OR name like '%holy smoke%'
    OR name like '%paprika%'
    OR name like '%juku%'
    OR name like '%neste%'
    OR name like '%abc%'
--     OR name like '%shi%'
    )
  and cgg.customerid is null


    insert into dw.customerCustomerGroups
SELECT c.id, 6, 100 --ryyppäys
from dw.customer c
         left join dw.customerCustomerGroups cgg on c.Id = cgg.customerid
where (name like '%alko%'
    OR name like '%teatro%'
    OR name like '%karma%'
    OR name like '%bar %'
    OR name like '%beach services%'
    OR name like '%wilson%'
    OR name like '%wanha mestari%'
    OR name like '%ilona%'
    OR name like '%avecra%'
    OR name like '%anraja oy%'
    OR name like '%alvin%'
    OR name like '%pupi%'
    OR name like '%ale bar%'
    OR name like '%amarillo%'
    OR name like '%aleksi salminen%'
--     OR name like '%shi%'
    )
  and cgg.customerid is null


    insert into dw.customerCustomerGroups
SELECT c.id, 7, 100 --terveydenhoito
from dw.customer c
         left join dw.customerCustomerGroups cgg on c.Id = cgg.customerid
where (name like '%apteek%'
    OR name like '%terveys%'
    OR name like '%sairaala%'
    )
  and cgg.customerid is null

    insert into dw.customerCustomerGroups
SELECT c.id, 8, 100 --viihde
from dw.customer c
         left join dw.customerCustomerGroups cgg on c.Id = cgg.customerid
where (name like '%blizzard%'
    OR name like '%spotify%'
    OR name like '%youtube%'
    OR name like '%bio marilyn%'
    OR name like '%netflix%'
    OR name like '%steam%'
    )
  and cgg.customerid is null

    insert into dw.customerCustomerGroups
SELECT c.id, 9, 100 --vaatekaupat
from dw.customer c
         left join dw.customerCustomerGroups cgg on c.Id = cgg.customerid
where (name like '%adidas%'
    OR name like '%hennes%'
    OR name like '%h&m%'
    OR name like '%bio marilyn%'
    OR name like '%netflix%'
    OR name like '%steam%'
    )
  and cgg.customerid is null
SELECT *
from dw.customer c
         left join dw.customerCustomerGroups ccg on c.Id = ccg.customerid
where ccg.customerid is null

    insert into dw.customerCustomerGroups
SELECT c.id, 10, 100 --vaatekaupat
from dw.customer c
         left join dw.customerCustomerGroups cgg on c.Id = cgg.customerid
where (name like '%taksi%'
    OR name like '%tier%'
    OR name like '%scooter%'
    OR name like '%ala-kokkila%'
    )
  and cgg.customerid is null


SELECT *
from dw.customer c
         left join dw.customerCustomerGroups ccg on c.Id = ccg.customerid
where ccg.customerid is null

-- DELETE from dw.customerCustomerGroups
-- where customerid in (select customerid from dw.customerCustomerGroups where customergroupdid = 3)

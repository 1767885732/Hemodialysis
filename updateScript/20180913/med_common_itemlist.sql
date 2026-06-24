update med_common_itemlist t set t.item_value='乙肝两对半'
where t.item_type='质控指标检验项目' and t.item_name='乙肝两对半';
commit;

update med_common_itemlist t set t.order_number=10
where t.item_type='检验项目明细配置' and t.item_name in('C—反应蛋白');
commit;

update med_common_itemlist t set t.order_number=t.order_number + 9
where t.item_type='检验项目明细配置' and t.item_name in('血常规(含有核红细胞五分类)');
commit;

update med_common_itemlist t set t.item_name='C—反应蛋白',t.item_value='C—反应蛋白'
where t.item_type='检验项目配置' and t.status='1' and t.item_name='超敏C反应蛋白';
commit;

update med_common_itemlist t set t.item_name='甲状旁腺素',t.item_value='甲状旁腺素'
where t.item_type='检验项目配置' and t.status='1' and t.item_name='甲状旁腺激素测定';
commit;
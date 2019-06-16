## 工作流系统数据库配置
* MySql创建数据库schema activiti
* 修改数据库配置 appsettings.json
```
"WorkflowDataSource"：{
    "providerName": "MySql",
    "database": "activiti",　//数据库名称
    "connectionString": "server=localhost;database=数据库名称;uid=用户名;pwd=密码;Character Set=utf8"
}
```

## 初始化数据库表结构
* 首次启动修改配置文件：resources\activiti.cfg.json 配置
  * databaseSchemaUpdate配置说明: 
    *  update：true 当数据库表结构发生变化时设置为true，系统会自动更新到新的表结构
    *  drop-create：系统会先删除表再创建表，该配置仅在开发时可用，nuget包引用该功能不可用
    *  create: 该配置仅在数据库为空时创建表结构，否则系统会无法启动，首次启动后请修改该配置为false，否则再次启动服务将无法启动。
*  
  ```
  {
    "id": "processEngineConfiguration",
    "type": "org.activiti.engine.impl.cfg.StandaloneProcessEngineConfiguration",
    //web主机应用程序名称
    "applicationName": "workflow",
    //数据库更新策略，true=update drop-create create
    "databaseSchemaUpdate": "数据库更新策略",
    ...
  ```

* 手工创建数据库表：
  * 创建表：
    * 流程运行时表结构： resources\db\create\activiti.mysql.create.engine.sql  
    * 流程历史记录表结构：resources\db\create\activiti.mysql.create.history.sql
  * 删除表
    * 流程运行时表结构： resources\db\drop\activiti.mysql.drop.engine.sql  
    * 流程历史记录表结构：resources\db\drop\activiti.mysql.drop.history.sql

## 简易流程编辑器访问地址 http://localhost:11015/index.html
* 编辑器代码 BpmnEditor

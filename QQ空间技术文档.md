# QQ空间技术文档

## 数据库设计

### 数据库关系图

![image](http://p1.bpimg.com/4851/ce04f692539d038b.png)

### 表

#### 用户

##### Users（用户）

![image](http://p1.bpimg.com/4851/1c4479b790c5e3b3.png)


列名 | 含义
---|---
userid | 用户id
usernumber | 用户账号（QQ号）
nickname | 用户昵称
zonename | 空间名称
password | 密码
sex | 性别
birthday | 生日
phonenumber | 手机号
address | 现居地
headimagename | 头像图片名
headimagepath | 头像图片路径
zonevisible | 空间是否可见
visitorvisible | 访客是否可见
commentable | 是否可评论

> 该表userid为主键

> zonevisible：all为所有人可见，friends为QQ好友可见，self为仅自己可见

> visitorvisible：同上

> commentable：同上

##### Friend（好友）

![image](http://i1.piimg.com/4851/c5bd67eae3a3276c.png)


列名 | 含义
---|---
userid | 用户id
friendid | 好友id
time | 添加时间
unable | 是否屏蔽

> userid：指提出好友申请的一方

> friendid：指接受好友申请的一方

##### Request（好友请求）

![image](http://i1.piimg.com/4851/4c5774fd547b2588.png)


列名 | 含义
---|---
userid | 用户id
friendid | 好友id
time | 添加时间

> userid：指提出好友申请的一方

> friendid：指接受好友申请的一方

#### 访客

##### Visitor（访客）

![image](http://p1.bqimg.com/4851/c2c6508277469f23.png)


列名 | 含义
---|---
id | 访客记录id
hostid | 空间主人id
userid | 访客id
time | 访问时间

> 该表id为主键

#### 相册

##### Album（相册）

![image](http://p1.bqimg.com/4851/1cbad24b93ea19ac.png)


列名 | 含义
---|---
albumid | 相册id
userid | 用户id
albumname | 相册名
albumcover | 相册封面
creattime | 创建时间

> 该表albumid为主键，userid为Users表外键

##### Photos（相片）

![image](http://p1.bqimg.com/4851/010382668573e724.png)


列名 | 含义
---|---
userid | 用户id
photoname | 相片名
photopath | 相片路径
albumid | 相册id
publishtime | 发布时间

> 该表userid为Users表外键

##### Albumcomment（相册评论）

![image](http://p1.bqimg.com/4851/1ef0f4cb948a1aad.png)


列名 | 含义
---|---
commentid | 评论id
albumid | 相册id
userid | 用户id
comment | 评论内容
publishtime | 发布时间

> 该表commentid为主键，userid为Users表外键

#### 说说

##### Saying（说说）

![image](http://p1.bqimg.com/4851/d7ee7c8dcbbf1a63.png)


列名 | 含义
---|---
sayingid | 说说id
userid | 用户id
sayingcontent | 说说内容
publishtime | 发布时间
praise | 点赞数
allow | 访问权限

> 该表sayingid为主键，userid为Users表外键

> allow：all为所有人可见，friends为QQ好友可见，self为仅自己可见，defined为自定义

##### SayingAllow（说说权限）

![image](http://p1.bqimg.com/4851/95054a9ceba84cf3.png)


列名 | 含义
---|---
id | 权限记录id
sayingid | 说说id
friendid | 该条说说可见的好友id

> 该表id为主键

##### Sayingcomment（说说评论）

![image](http://i1.piimg.com/4851/306a5542a6a3d9f3.png)


列名 | 含义
---|---
commentid | 评论id
sayingid | 说说id
userid | 用户id
comment | 评论内容
publishtime | 发表时间

> 该表commentid为主键，sayingid为Saying表外键，userid为Users表外键

##### Commentreply（说说评论回复）

![image](http://i1.piimg.com/4851/23c32a1cafab595e.png)


列名 | 含义
---|---
replyid | 回复id
commentid | 评论id
authorid | 发布评论的用户id
userid | 发布回复的用户id
reply | 回复内容
publishtime | 发表时间

> 该表replyid为主键，commentid为Comment表外键，userid为Users表外键

##### Praise（点赞）

![image](http://p1.bqimg.com/4851/7ebe0c30978abcf1.png)


列名 | 含义
---|---
praiseid | 点赞记录id
sayingid | 说说id
userid | 点赞用户id

> 该表praiseid为主键，userid为Users表外键

#### 日志

##### Log（日志）

![image](http://p1.bqimg.com/4851/5e9dd9c545643f3d.png)


列名 | 含义
---|---
logid | 日志id
userid | 用户id
headline | 标题
logcontent | 日志内容
publishtime | 发表时间

> 该表logid为主键，userid为Users表外键

##### Logcomment（日志评论）

![image](http://p1.bqimg.com/4851/2396f9bb6aa84f95.png)

列名 | 含义
---|---
commentid | 评论id
logid | 日志id
userid | 用户id
comment | 评论内容
publishtime | 发表时间

> 该表commentid为主键，logid为Log表外键，userid为Users表外键

#### 留言

##### Message（留言）

![image](http://p1.bqimg.com/4851/0bbf0e6b2a6270db.png)

列名 | 含义
---|---
messageid | 留言id
hostid | 空间主人id
userid | 留言用户id
messagecontent | 留言内容
publishtime | 留言时间

> 该表messageid为主键，userid为Users表外键

##### Messagecomment（留言评论）

![image](http://p1.bqimg.com/4851/a29e0663de1f9785.png)


列名 | 含义
---|---
commentid | 评论id
messageid | 留言id
userid | 用户id
comment | 评论内容
publishtime | 发表时间

> 该表commentid为主键，userid为Users表外键，messageid为Message表外键

#### 标签

##### Lable（标签）

![image](http://p1.bqimg.com/4851/c86e386b19ba73cf.png)


列名 | 含义
---|---
id | 标签记录id
userid | 标签主人id
friendid | 好友id
lablename | 标签名

> 该表id为主键，userid为Users表外键

### 视图

> 以下列名含义不再一一列举，可参考以上表格

#### FriendRequest（好友请求）

![image](http://p1.bqimg.com/4851/dd2a0b10405e34f4.png)

#### UsersAlbum（相册评论）

![image](http://p1.bqimg.com/4851/11d0136f6c7c34a9.png)

#### UsersComment（说说评论）

![image](http://i1.piimg.com/4851/4fad8bb01d738374.png)

#### UsersLog（日志）

![image](http://i1.piimg.com/4851/c488414b206fd416.png)

#### UsersMessage（留言）

![image](http://i1.piimg.com/4851/ceba59c560120286.png)

#### MessageComment（留言回复）

![image](http://i1.piimg.com/4851/a4d26054128649da.png)

#### UsersReply（说说回复）

![image](http://i1.piimg.com/4851/0c5b145c35a18bf2.png)

#### UsersSaying（说说）

![image](http://i1.piimg.com/4851/0c5b145c35a18bf2.png)

#### UsersVisitor（访客）

![image](http://i1.piimg.com/4851/5c63b896cc1b37ea.png)

## 使用方法


方法名 | 参数类型 | 返回值类型 | 功能简介
---|---|---|---
Save | string sql | 无 | 对数据库进行增、删、减操作
Select | string sql | int | 对数据库进行查找操作，找到则返回1，没找到则返回0
DataT | string sql | datatable | 对数据库进行查找操作，并返回所查找到的数据库表
GenerateRandomNumber | int Length | string | 生成随机数
CheckAbleToComment | int userid,int friendid | int | 判断评论权限
CheckVisitorVisible | int userid,int friendid | int | 判断查看访客权限




































# MonoAppCache

利用C#特性——索引器  构建了cache（mono for 移动app 是没有cache功能的）。
不过是用XML模拟的，也就是最终序列化成了XML文档。
开发平台及工具：Mac mini，monoDevelop
代码所兼容平台：contos 6.5+，windows7+，iphone6.0+，android4.0+，wp8+,以及mac。以上适用版本只是当时用于测试的版本模拟器，最低版本没测。

调用示例如下：
存值：AppCaches.AppCache["user"] = user对象。
取值：User user = AppCaches.AppCache["user"]。
代码还需要调整

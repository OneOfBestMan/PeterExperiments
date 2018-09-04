#全局管理配置类
#   获取的文件类型，视频，图片
#   链接探测最深的层级
#   访问的顶级URL

class TaskOption(object):
      def __init__(self, fileType, maxLevel, baseUrl, loginUrl):
        self.fileType = fileType
        self.maxLevel = maxLevel
        self.baseUrl = baseUrl
        self.loginUrl= loginUrl
 
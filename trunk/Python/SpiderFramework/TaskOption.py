#全局管理配置类
#   获取的文件类型，视频，图片
#   链接探测最深的层级
#   访问的顶级URL

class TaskOption(object):
      def __init__(self, baseUrl, loginUrl,beginUrl,fileType, maxLevel, headers,reTargetUrl):
        self.baseUrl = baseUrl
        self.loginUrl= loginUrl
        self.beginUrl=beginUrl
        self.fileType = fileType
        self.maxLevel = maxLevel

        self.headers= headers
        self.reTargetUrl=reTargetUrl

 
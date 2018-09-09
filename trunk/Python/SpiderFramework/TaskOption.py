#全局管理配置类
#   获取的文件类型，视频，图片
#   链接探测最深的层级
#   访问的顶级URL

class TaskOption(object):
      def __init__(self, baseUrl, loginUrl,beginUrl,fileType, maxLevel, headers,reTargetUrl,reRetchUrls,reFiles,fetchUrlMustContain):
        #url
        self.baseUrl = baseUrl
        self.loginUrl= loginUrl
        self.beginUrl=beginUrl
        self.headers= headers


        # 搜索的文件类型
        self.fileType = fileType
        self.maxLevel = maxLevel
        self.reRetchUrls=reRetchUrls
        self.fetchUrlMustContain=fetchUrlMustContain
        self.reFiles=reFiles
        self.reTargetUrl=reTargetUrl

 
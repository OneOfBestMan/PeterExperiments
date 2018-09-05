#全局管理类

class UrlDto(object):
      def __init__(self,name,url,level,isVisited):
          self.name=name
          self.url=url
          self.level=level
          self.isVisited=isVisited

class Manager(object):
      
      #所有Url数组，对象UrlDto的数组
      Urls=[]

      def AddUrl(self,urlDto):
          if urlDto in Manager.Urls:
               print(url+"已存在")
          else:
               Manager.Urls.append(urlDto)

      def AddUrls(self,urlDtos):
          for dto in urlDtos:
              AddUrl(url,dto)

      def RemoveUrl(self,url):
          for dto in Manager.Urls:
              if dto.url==url:
                 Manager.Urls.remove(dto)



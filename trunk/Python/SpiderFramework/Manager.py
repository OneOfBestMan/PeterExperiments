#全局管理类

class UrlDto(object):
      def __init__(self,name,url,level,isVisited):
          self.name=name
          self.url=url
          self.level=level
          self.isVisited=isVisited

class Manager(object):
      
      #所有Url数组，对象UrlDto的数组
      def __init__(self):
          self.Urls=[]

      def AddUrl(self,urlDto):
          if urlDto in self.Urls:
               print(url+"已存在")
          else:
               self.Urls.append(urlDto)

      def AddUrls(self,urlDtos):
          for dto in urlDtos:
              self.AddUrl(dto)

      def RemoveUrl(self,url):
          for dto in self.Urls:
              if dto.url==url:
                 self.Urls.remove(dto)



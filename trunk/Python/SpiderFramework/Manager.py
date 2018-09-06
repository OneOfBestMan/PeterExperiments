#全局管理类
#from globalData import *
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
          self.Dtos=[]

      def AddUrl(self,urlDto):
          if urlDto.url in self.Urls:
               print(url+"已存在")
          else:
               self.Urls.append(urlDto.url)
               self.Dtos.append(urlDto)

      def AddUrls(self,urlDtos):
          for dto in urlDtos:
              self.AddUrl(dto)

      def RemoveUrl(self,url):
          for url in self.Urls:
              self.Urls.remove(url)



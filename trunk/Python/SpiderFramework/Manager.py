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

      def AddUrl(self,url,name,level):
          if url in Urls:
               print(url+"已存在")
          else:
               Urls.append(UrlDto(name,url,level,False))

      def RemoveUrl(self,url):
          if url in Urls:
               print(url+"已存在")
          else:
               Urls.append(UrlDto(name,url,level,False))


#全局管理类
#from globalData import *
import requests
import BaseRequest
import TaskOption
import time
class UrlDto(object):
      def __init__(self,name,url,level,isVisited,isDownload):
          self.name=name
          self.url=url
          self.level=level
          self.isVisited=isVisited
          self.isDownload=isDownload

class Manager(object):
      
      #所有Url数组，对象UrlDto的数组
      def __init__(self,cookie,option):
          self.Urls=[]
          self.Dtos=[]
          self.cookie=cookie
          self.option=option

      def AddUrl(self,urlDto):
          if urlDto.url in self.Urls:
               print(urlDto.url+"已存在")
          else:
               print(urlDto.url+"添加")
               self.Urls.append(urlDto.url)
               self.Dtos.append(urlDto)
               self.getThisUrl(urlDto)

      def AddUrls(self,urlDtos):
          for dto in urlDtos:
              self.AddUrl(dto)

      def RemoveUrl(self,url):
          for url in self.Urls:
              self.Urls.remove(url)

      def getThisUrl(self,urldto):
          time.sleep(1)
          print("获取:"+urldto.name+',level='+str(urldto.level)+",url="+urldto.url)
          request=BaseRequest.BaseRequest(self.option)
          html=request.getPage(urldto.url,self.cookie)
          urls=request.getSearchUrlsFromPage(html)
          urldto.isVisited=True
          dtos=[]
          if len(urls) >0  and urldto.level < self.option.maxLevel:
             for k,v in urls.items():
                 dto=UrlDto(k,v,urldto.level+1,False,False)
                 dtos.append(dto)
             self.AddUrls(dtos)


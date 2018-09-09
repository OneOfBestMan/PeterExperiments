#全局管理类

from globalData import *
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
          self.cookie=cookie
          self.option=option
          #self.request=requests.session();
          self.request=BaseRequest.BaseRequest(option,cookie)

      def AddUrl(self,urlDto):
          if urlDto.url in glo_urls:
               print(urlDto.url+"已存在")
          else:
               print(urlDto.url+"添加")
               glo_urls.append(urlDto.url)
               glo_dtos.append(urlDto)
               urlDto.isVisited=True
               self.getThisUrl(urlDto)

      def AddUrls(self,urlDtos):
          for dto in urlDtos:
              self.AddUrl(dto)

      def RemoveUrl(self,url):
          for url in glo_urls:
              glo_urls.remove(url)

      def getThisUrl(self,urldto):
          print("获取:"+urldto.name+',level='+str(urldto.level)+",url="+urldto.url)
          
          html=self.request.getPage(urldto.url)
          urls=self.request.getSearchUrlsFromPage(html)
          #urldto.isVisited=True
          dtos=[]
          if len(urls) >0  and urldto.level < self.option.maxLevel:
             for key in urls.keys():
                 dto=UrlDto(key,urls[key],urldto.level+1,False,False)
                 dtos.append(dto)
             self.AddUrls(dtos)


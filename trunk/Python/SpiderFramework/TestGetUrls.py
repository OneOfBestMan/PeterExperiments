
import TaskOption
import CockieProvider
import Manager
import BaseRequest
import time
import os
import threading

class TestGetUrls(object):
      
      headers= {
                 'Accept': "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
                 'Accept - Encoding': "gzip, deflate, br",
                 'Connection': "Keep-Alive",
                 'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36'
                 #'Cookie': self.cookies
               }
      option = TaskOption.TaskOption("https://www.youngmaker.com/", "https://www.youngmaker.com/","https://www.youngmaker.com/home/courses/study/catid/292/capid/7255/classroomid/29/sectionid/2100/ccid/14.html","mp4", 5, headers,"")
      cookie_dict=dict()


      def startSplider(self):
          level=1
          login_name = '13501884996'
          login_password = 'youngmaker22'
          provider=CockieProvider.CockieProvider(TestGetUrls.option)
          cookie_dict=provider.login(login_name,login_password)

          dto=Manager.UrlDto('起始链接',TestGetUrls.option.beginUrl,1,False)
          manager=Manager.Manager(cookie_dict,TestGetUrls.option)
          manager.AddUrl(dto)
          #self.getThisUrl(dto,cookie_dict)
          #self.getLoopManagerUrls(cookie_dict)

          for dto in  manager.Dtos:
              print(dto.name+":"+dto.url)
          #files=request.downloadUrlsFromPage(html,cookie_dict)

      def getThisUrl(self,dto,cookie):
          print("获取:"+dto.name+',level='+str(dto.level)+",url="+dto.url)
          request=BaseRequest.BaseRequest(TestGetUrls.option)
          html=request.getPage(dto.url,cookie)
          urls=request.getSearchUrlsFromPage(html)
          dto.isVisited=True
          dtos=[]
          if len(urls) >0  and dto.level< 5:
             for key in urls.keys():
                 if "course" in urls[key] or "Course" in urls[key]: 
                     dto=Manager.UrlDto(key,urls[key],dto.level,False)
                     dtos.append(dto)
             TestGetUrls.manager.AddUrls(dtos)
             
      def _getNotVisitUrlPredict(self,dto):
          return dto.isVisited==False;

      def getLoopManagerUrls(self,cookie):
          urlsfiltered=filter(self._getNotVisitUrlPredict, TestGetUrls.manager.Dtos)
          urlsToLoop=list(urlsfiltered)
          try:
              while urlsToLoop and len(urlsToLoop) >0:
                for dto in urlsToLoop:
                    dto.level=dto.level+1
                    dto.isVisited=True
                    self.getThisUrl(dto,cookie)
                time.sleep(1)
                urlsToLoop=filter(self._getNotVisitUrlPredict, TestGetUrls.manager.Dtos)
          except Exception:
                print("可能没有连接了!")
                time.sleep(3)
                self.getLoopManagerUrls(cookie)











#测试用request获取一个页面，并且下载其中的一个文件
#1:第一次打开页面，并且获取文件
#2:第一次打开页面，获取页面的所有链接；第二次打开页面从页面中下载文件
#结论：两次都可以下载到文件，说明和打开几次那个页面没有关系
#2.1：第一次打开用一个request,第二次下载用另外一个requests
#结论，使用BaseRequest两个实例，第一次打开页面，第二次再次打开下载，可以下载文件

import BaseRequest
import TaskOption
import CockieProvider
import os
import datetime

class requestsTest(object):
      headers= {
                 'Accept': "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
                 'Accept - Encoding': "gzip, deflate, br",
                 'Connection': "Keep-Alive",
                 'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36'
                 #'Cookie': self.cookies
               }
      baseUrl="https://www.youngmaker.com"
      urlBegin="https://www.youngmaker.com/home/courses/study/catid/292/capid/7255/classroomid/29/sectionid/2100/ccid/14.html"
      
      reA = '<a.*?href="(.+?)".*?>(.+?)</a>'
      #reDownload = 'src="(.*?\.mp4)"'
      reDownload = 'src="//([^"]+)"'
      option = TaskOption.TaskOption(baseUrl, baseUrl,urlBegin,"mp4", 5, headers,"",reA,reDownload)

      def getCookie(self):
          login_name = '13501884996'
          login_password = 'youngmaker22'
          provider=CockieProvider.CockieProvider(requestsTest.option)
          cookie=provider.login(login_name,login_password)
          return cookie

      def manager(self):
          cookie=self.getCookie()

          url1="https://www.youngmaker.com/home/courses/study/capid/7261/catid/292/sectionid/2101/classroomid/29/ccid/14.html"
          url2="https://www.youngmaker.com/home/courses/study/capid/15065/catid/292/sectionid/2140/classroomid/29/ccid/14.html"
          #self.requestOnce(cookie,url2)
          #self.requestTwice(cookie,url1)
          self.requestTwiceWithDiffRequests(cookie,url1)

      def requestOnce(self,cookie,url2):
          request=BaseRequest.BaseRequest(requestsTest.option,cookie)
          #html=request.getPage(url2)
          dir=datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
          os.mkdir(dir) 
          request.downloadFromUrls(url2,dir)

      def requestTwice(self,cookie,url1):
          request=BaseRequest.BaseRequest(requestsTest.option,cookie)
          html=request.getPage(url1)
          dir=datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
          os.mkdir(dir) 
          request.downloadFromUrls(url1,dir)

      def requestTwiceWithDiffRequests(self,cookie,url1):
          request=BaseRequest.BaseRequest(requestsTest.option,cookie)
          html=request.getPage(url1)
          dir=datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
          os.mkdir(dir) 
          request1=BaseRequest.BaseRequest(requestsTest.option,cookie)
          request1.downloadFromUrls(url1,dir)

t=requestsTest()
t.manager()



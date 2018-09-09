
#测试在获取到cookie后，1分钟，5分钟，10分钟,20分钟后再次获取页面并且下载
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
import time

class requestsCoocieExpiredTest(object):
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
      fetchUrlMustContain=""
      option = TaskOption.TaskOption(baseUrl, baseUrl,urlBegin,"mp4", 5, headers,"",reA,reDownload,fetchUrlMustContain)

      def getCookie(self):
          login_name = '13501884996'
          login_password = 'youngmaker22'
          provider=CockieProvider.CockieProvider(requestsCoocieExpiredTest.option)
          cookie=provider.login(login_name,login_password)
          return cookie

      def manager(self):
          for num in range(1,5):
              print("num="+str(num))
          cookie=self.getCookie()

          url1="https://www.youngmaker.com/home/courses/study/capid/7261/catid/292/sectionid/2101/classroomid/29/ccid/14.html"
          url2="https://www.youngmaker.com/home/courses/study/capid/15065/catid/292/sectionid/2140/classroomid/29/ccid/14.html"
          url3="https://www.youngmaker.com/home/courses/study/capid/7262/catid/292/sectionid/2101/classroomid/29/ccid/14.html"
          url4="https://www.youngmaker.com/home/courses/study/capid/14010/catid/292/sectionid/2102/classroomid/29/ccid/14.html"
          url5="https://www.youngmaker.com/home/courses/study/capid/14041/catid/292/sectionid/2102/classroomid/29/ccid/14.html"
          url6="https://www.youngmaker.com/home/courses/study/capid/14969/catid/292/sectionid/2140/classroomid/29/ccid/14.html"
          urls=[url1,url2,url3,url4,url5,url6]


          dir=datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
          os.mkdir(dir) 
          #tempnum=5
          for num in range(1,6):
              time.sleep(2**num*5*61)
              self.requestOnce(cookie,urls[num],dir)
              print("waiting for "+str(num*5*60)+" seconds")


      def requestOnce(self,cookie,url,dir):
          request=BaseRequest.BaseRequest(requestsCoocieExpiredTest.option,cookie)
          #html=request.getPage(url)
          request.downloadFromUrls(url,dir)

      def requestTwice(self,cookie,url1):
          request=BaseRequest.BaseRequest(requestsCoocieExpiredTest.option,cookie)
          html=request.getPage(url1)
          dir=datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
          os.mkdir(dir) 
          request.downloadFromUrls(url1,dir)

      def requestTwiceWithDiffRequests(self,cookie,url1):
          request=BaseRequest.BaseRequest(requestsCoocieExpiredTest.option,cookie)
          html=request.getPage(url1)
          dir=datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
          os.mkdir(dir) 
          request1=BaseRequest.BaseRequest(requestsCoocieExpiredTest.option,cookie)
          request1.downloadFromUrls(url1,dir)

t=requestsCoocieExpiredTest()
t.manager()


#DevTools listening on ws://127.0.0.1:12729/devtools/browser/65bd9a65-fc7f-4c0f-8828-e7bfb26a36ca
#下载文件:1524020111s7xBc.mp4
#waiting for 300 seconds
#下载文件:1510825339J8sfj.mp4
#waiting for 600 seconds
#下载文件:15240197796sGry.mp4
#waiting for 900 seconds
#下载文件:1524019803RKGCP.mp4
#waiting for 1200 seconds
#Press any key to continue . . .
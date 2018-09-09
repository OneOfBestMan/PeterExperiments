
import TaskOption
import CockieProvider
import Manager
import BaseRequest
import time
import os
import threading
import datetime
from globalData import *


class TaskManager(object):
      
      headers= {
                 'Accept': "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
                 'Accept - Encoding': "gzip, deflate, br",
                 'Connection': "Keep-Alive",
                 'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36'
                 #'Cookie': self.cookies
               }

      baseUrl="https://www.youngmaker.com"
      urlBegin="https://www.youngmaker.com/home/courses/study/catid/1507/capid/50774/classroomid/685/sectionid/7201/ccid/14.html"
      
      #reA = '<a.*?href="(.+?)".*?>(.+?)</a>'
      reA = '<a.*?href="([^\"]*)".*?>(.*?)</a>'
      #reDownload = 'src="(.*?\.mp4)"'
      reDownload = 'src="//([^"]+)"'
      fetchUrlMustContain="classroomid"
      option = TaskOption.TaskOption(baseUrl, baseUrl,urlBegin,"mp4", 5, headers,"",reA,reDownload,fetchUrlMustContain)


      def startSplider(self):
          login_name = '13501884996'
          login_password = 'youngmaker22'
          provider=CockieProvider.CockieProvider(TaskManager.option)
          cookie=provider.login(login_name,login_password)

          fetchUrlThread =threading.Thread(target=self.getAllUrlsThread,args=(cookie,))
          fetchUrlThread.start()
          
          dir=datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
          os.mkdir(dir) 
          request=BaseRequest.BaseRequest(TaskManager.option,cookie)
          downloadThread =threading.Thread(target=self.download,args=(request,dir))
          downloadThread.start()

      def getAllUrlsThread(self,cookie):
          print("获取链接线程开始")
          manager=Manager.Manager(cookie,TaskManager.option)
          dto=Manager.UrlDto('起始链接',TaskManager.option.beginUrl,1,False,False)
          manager.AddUrl(dto)

      def download(self,request,dir):
          print("下载线程开始")
          try:
              while glo_dtos and len(glo_dtos) >0:
                    for dto in glo_dtos:
                        if dto.isDownload==True:
                           continue
                        dto.isDownload=True
                        print("开始下载文件："+dto.name+"="+dto.url+"，level="+str(dto.level))
                        request.downloadFromUrls(dto.url,dir)
                        time.sleep(1)
              print("正常，可能没有下载连接了!")
              time.sleep(3)
              self.download(request,dir)
          except Exception as e:
                 print("出错，可能没有下载连接了,"+repr(e))
                 time.sleep(3)
                 self.download(request,dir)



 
 












import TaskOption
import CockieProvider
import Manager
import BaseRequest
import time
import os
import threading

class TaskManager(object):
      
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
          login_name = '13501884996'
          login_password = 'youngmaker22'
          provider=CockieProvider.CockieProvider(TaskManager.option)
          cookie_dict=provider.login(login_name,login_password)

          manager=Manager.Manager(cookie_dict,TaskManager.option)

          fetchUrlThread =threading.Thread(target=self.getAllUrlsThread,args=(manager,))
          fetchUrlThread.start()

          downloadThread =threading.Thread(target=self.download,args=(manager,cookie_dict))
          downloadThread.start()

          #fetchUrlThread.join()
          #downloadThread.join()
          #self.getAllUrlsThread(manager)
          #self.download(manager,cookie_dict)

      def getAllUrlsThread(self,manager):
          print("获取链接线程开始")
          dto=Manager.UrlDto('起始链接',TaskManager.option.beginUrl,1,False,False)
          manager.AddUrl(dto)

      def download(self,manager,cookie):
          print("下载线程开始")
          request=BaseRequest.BaseRequest(TaskManager.option)
          dtosfiltered=filter(lambda d:d.isDownload==False,manager.Dtos)
          dtosToLoop=list(dtosfiltered)
          try:
              while dtosToLoop and len(dtosToLoop) >0:
                    for dto in dtosToLoop:
                        dto.isDownload=True
                        print("下载文件："+dto.name+"="+dto.url+"，level="+str(dto.level))
                        request.downloadFromUrls(dto.url,cookie)
                    time.sleep(1)
                    dtosfiltered=filter(lambda d:d.isDownload==False,manager.Dtos)
                    dtosToLoop=list(dtosfiltered)
              print("可能没有下载连接了!")
              time.sleep(3)
              self.download(manager,cookie)
          except Exception:
                print("可能没有下载连接了!")
                time.sleep(3)
                self.download(manager,cookie)











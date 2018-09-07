   #获取一个页面
   #获取一个页面的某个对象链接
   #下载文件
import requests
import re
import os
import datetime

class BaseRequest(object):
      
      def __init__(self,taskOption):
          self.taskOption=taskOption

      def getPage(self,url,req_cookies):
          content=requests.get(url=url, headers=self.taskOption.headers, cookies=req_cookies)
          return content.text

      def getSearchUrlsFromPage(self,html):
          reA = '<a.*?href="([^\"]*)".*?>(.*?)</a>'
          urls=re.findall(reA,html, re.I|re.S|re.M)
          anchors= dict()
          dr = re.compile(r'<[^>]+>',re.S)
          for url in urls:
              key=dr.sub('',url[1]).replace("\r","").replace("\n","")
              intourl=''
              if  'http' in url[0]:
                  intourl=url[0]
              else:
                  intourl=self.taskOption.baseUrl+ url[0]
              if intourl not in anchors.values():
                 anchors[key]=intourl
          return anchors;

      def downloadFromUrls(self,url,req_cookies):
          print("下载链接："+url)
          html=self.getPage(url,req_cookies)
          self.downloadFromPage(html,req_cookies)

      def _downloadFromPage(self,html,req_cookies):
          reA = 'src="//([^"]+)"'
          downloadurls = re.findall(reA,html, re.I|re.S|re.M)
          #dr = re.compile(r'<[^>]+>',re.S)
          dir=datetime.datetime.now().strftime("%Y%m%d_%H%M%S")
          os.mkdir(dir) 
          for url in downloadurls:
              #key=dr.sub('',url[1]).replace("\r","").replace("\n","")
              name=os.path.basename(url)
              if  'http' in url[0]:
                   self._download(dir,name,url,req_cookies)
              else:
                  url=self.taskOption.baseUrl+ url[0]
                  self._download(dir,name,url,req_cookies)

      def _download(self,dir,filename, url,req_cookies):
          try:
             r=requests.get(url)
             with open(dir+'/'+filename,"wb") as f:
                 f.write(r.content)
                 print("下载文件"+filename)
             f.close()
          except Exception:# 异常基类
                 print("可能没有文件下载 !")




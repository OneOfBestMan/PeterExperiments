   #获取一个页面
   #获取一个页面的某个对象链接
   #下载文件
import requests
import re
import os

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
              if  'http' in url[0]:
                  anchors[key]=url[0]
              else:
                  anchors[key]=self.taskOption.baseUrl+ url[0]
          return anchors;

      def downloadUrlsFromPage(self,html,req_cookies):
          reA = 'src="//([^"]+)"'
          downloadurls = re.findall(reA,html, re.I|re.S|re.M)
          #dr = re.compile(r'<[^>]+>',re.S)
          for url in downloadurls:
              #key=dr.sub('',url[1]).replace("\r","").replace("\n","")
              name=os.path.basename(url)
              if  'http' in url[0]:
                   self.download(name,url,req_cookies)
              else:
                  url=self.taskOption.baseUrl+ url[0]
                  self.download(name,url,req_cookies)
          return anchors;

      def download(self,filename, url,req_cookies):
          try:
             r=requests.get(url)
             with open(filename,"wb") as f:
                 f.write(r.content)
             f.close()
          except Exception:# 异常基类
                 print("可能没有文件下载 !")




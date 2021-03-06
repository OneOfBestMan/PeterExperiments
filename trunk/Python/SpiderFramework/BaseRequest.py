   #获取一个页面
   #获取一个页面的某个对象链接
   #下载文件
import requests
import re
import os
import datetime

class BaseRequest(object):
      
      def __init__(self,taskOption,cookie):
          self.taskOption=taskOption
          self.cookie=cookie
          self.sessionRequest=requests.Session()

      def getPage(self,url):
          content=self.sessionRequest.get(url=url, headers=self.taskOption.headers, cookies=self.cookie)
          return content.text

      def getSearchUrlsFromPage(self,html):
          reA = self.taskOption.reRetchUrls
          urls=re.findall(reA,html, re.I|re.S|re.M)
          anchors= dict()
          dr = re.compile(r'<[^>]+>',re.S)
          for url in urls:
              if self.taskOption.fetchUrlMustContain  not in url[0]:
                 continue
              key=dr.sub('',url[1]).replace("\r","").replace("\n","")
              intourl=''
              if  'http' in url[0]:
                  intourl=url[0]
              else:
                  intourl=self.taskOption.baseUrl+ url[0]
              if intourl not in anchors.values():
                 anchors[key]=intourl.replace(" ", "")
          return anchors;

      #dto一个代表需要搜索的对象
      #这个对象的名称，可以给由这个对象找到的页面中的目标元素的父名称
      def downloadFromUrls(self,dto,dir):
          html=self.getPage(dto.url)
          self._downloadFromPage(html,dto,dir)

      def _downloadFromPage(self,html,dto,dir):
          reA =self.taskOption.reFiles
          downloadurls = re.findall(reA,html, re.I|re.S|re.M)
          for url in downloadurls:
              url=url.replace("\r","").replace("\n","").replace("\t","")
              name=os.path.basename(url)
              if 'http' not in url and 'com' not in url:
                   url=self.taskOption.baseUrl+ url
              if 'http' not in url:
                   url="https://"+url
              self._download(dto,dir,name,url)

      def _download(self,dto,dir,filename, url):
          try:
             r=self.sessionRequest.get(url)
             with open(dir+'/'+dto.name +"_"+filename,"wb") as f:
                 f.write(r.content)
                 print("下载文件:"+dto.name+"_"+filename)
             f.close()
          except Exception as e:# 异常基类
                 print("下载文件出错,"+e.reason)





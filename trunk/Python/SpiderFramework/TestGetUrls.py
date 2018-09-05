
import TaskOption
import CockieProvider
import Manager
import BaseRequest


class TestGetUrls(object):
      
      headers= {
                 'Accept': "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
                 'Accept - Encoding': "gzip, deflate, br",
                 'Connection': "Keep-Alive",
                 'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36'
                 #'Cookie': self.cookies
               }
      option = TaskOption.TaskOption("https://www.youngmaker.com/", "https://www.youngmaker.com/",'https://www.youngmaker.com/home/courses/study/capid/7255/catid/292/sectionid/2100/classroomid/29/ccid/14.html',"mp4", 5, headers,"")
      cookie_dict=dict()
      manager=Manager.Manager()

      def startSplider(self):
          level=1
          login_name = '13501884996'
          login_password = 'youngmaker22'
          provider=CockieProvider.CockieProvider(TestGetUrls.option)
          cookie_dict=provider.login(login_name,login_password)
          self.getAllUrls(TestGetUrls.option.beginUrl,1,cookie_dict)
          for dto in TestGetUrls.manager.Urls:
              print(dto.name+":"+dto.url)
          #files=request.downloadUrlsFromPage(html,cookie_dict)

      def getAllUrls(self,baseUrl,level,cookie):
          print("获取url"+baseUrl+',level='+str(level))
          request=BaseRequest.BaseRequest(TestGetUrls.option)
          html=request.getPage(baseUrl,cookie)
          urls=request.getSearchUrlsFromPage(html)
          dtos=[]
          if len(urls) >0  and level< 5:
             for key in urls.keys():
                 if "course" in urls[key] or "Course" in urls[key]: 
                     dto=Manager.UrlDto(key,urls[key],level,False)
                     dtos.append(dto)
             TestGetUrls.manager.AddUrls(dtos)
             for key in urls.keys():
                 if "course" in urls[key] or "Course" in urls[key]:
                     self.getAllUrls(urls[key],level+1,cookie)








import TaskOption
import CockieProvider
import Manager

class TestGetUrls(object):
      
      headers= {
                 'Accept': "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
                 'Accept - Encoding': "gzip, deflate, br",
                 'Connection': "Keep-Alive",
                 'User-Agent': self.user_agent,
                 #'Cookie': self.cookies
               }
      option = TaskOption("https://www.youngmaker.com/", "https://www.youngmaker.com/",'https://www.youngmaker.com/home/courses/study/capid/7255/catid/292/sectionid/2100/classroomid/29/ccid/14.html',"mp4", 5, headers,"")
      cookie_dict=dict()
      manager=Manager()

      def startSplider():
          login_name = '13501884996'
          login_password = 'youngmaker22'
          provider=CockieProvider(option)
          cookie_dict=provider.login(login_name,login_password)





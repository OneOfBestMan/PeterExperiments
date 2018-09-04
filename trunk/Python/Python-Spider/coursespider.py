
# 引入python自带的http请求库
#from urllib import request
import re
import requests
import os
import http.cookiejar
from selenium import webdriver
import time
# mock http request 
# 正则\s\S匹配所有字符 *代表匹配0次或无线多次 ?代表非贪婪模式 () 代表组 正则匹配之后只取到需要的数据
class CourseSpider():
    urlLogin='https://www.youngmaker.com/home/user/newlogin.html'
    urlbase='https://www.youngmaker.com'
    url = 'https://www.youngmaker.com/home/courses/study/capid/7255/catid/292/sectionid/2100/classroomid/29/ccid/14.html'
    # 匹配所有符合的大的节点
    VIDEOALL = '<div class="mulu" ([\s\S]*?)</div>'
    VIDEOURL = 'src="//([^"]+)"'
    # 匹配名称
 
   # VIDEOA = 'href="([^\"]*)"'
    VIDEOA = '<a.*?href="([^\"]*)".*?>(.*?)</a>'
    # 所有的目标链接
    COURSEURL =[]
	# 所有的链接
    def __init__(self):
        self.my_url = 'https://www.youngmaker.com/home/courses/study/capid/7255/catid/292/sectionid/2100/classroomid/29/ccid/14.html'
        self.user_agent = 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36'
        self.cookies="yjs_id=62f2ad27c21284e070a7b57da16fbb62; PHPSESSID=af9p6e707co4hcbl3vjo3e178s; CNZZDATA1262355882=719981530-1535934345-%7C1535934345; ctrl_time=1"
        self.cookie_dir = 'D:/cookie_youngmaker.txt'
        self.user_headers = {
            'Accept': "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
            'Accept - Encoding': "gzip, deflate, br",
            'Connection': "Keep-Alive",
            'User-Agent': self.user_agent,
            #'Cookie': self.cookies
        }

    def login(self,name, passwd):
    # 这里可以用Chrome、Phantomjs等，如果没有加入环境变量，需要指定具体的位置
        login_name = name
        login_password = passwd
        driver = webdriver.Chrome()
        driver.get(CourseSpider.urlbase)
        time.sleep(1)

        login_button = driver.find_element_by_id('login_b')

        login_button.click()
        time.sleep(3)

        driver.switch_to_frame("layui-layer-iframe1")
        time.sleep(1)

        username = driver.find_element_by_id('inputEmail')
        username.clear()
        password = driver.find_element_by_id('inputPassword')
        password.clear()
        username.send_keys(login_name)
        password.send_keys(login_password)

        submit = driver.find_element_by_css_selector('button[type=\"button\"]')
        time.sleep(1)
        submit.click()
        time.sleep(3)
        return driver.get_cookies()

    def sele2req_cookie(self,cookies):
        cookie_dict = dict()
        for cookie in cookies:
            cookie_dict[cookie['name']] = cookie['value']
        return cookie_dict

    def __fetch_content(self,req_cookies,urlRequest):
        content=requests.get(url=urlRequest, headers=self.user_headers, cookies=req_cookies)
        return content.text

    def __getcourseurl(self,htmls):
        courseurl=re.findall(CourseSpider.VIDEOALL,htmls)
        urls=re.findall(CourseSpider.VIDEOA,courseurl[0], re.I|re.S|re.M)
        anchors= dict()
        dr = re.compile(r'<[^>]+>',re.S)
        for url in urls:
            key=dr.sub('',url[1]).replace("\r","").replace("\n","")
            anchors[key]=CourseSpider.urlbase+ url[0]
        return anchors;

    # 数据精炼 去掉空格和换行符
    def __refine(self, anchors):
        l = lambda anchor: {
          'name': anchor['name'][0].strip(),
          'number': anchor['number'][0]
          }
        return map(l, anchors)

    # 打印排序的结果
    def __show(self, anchors):
        for anchor in anchors:
            print(anchor)

    def __downloadAll(self, anchors,req_cookies):
        for key in anchors.keys():
            self.__down(key,anchors[key],req_cookies)

    def __down(self,key, url,req_cookies):
        try:
            html = self.__fetch_content(req_cookies,url)
            videourl=re.findall(CourseSpider.VIDEOURL,html, re.I|re.S|re.M)[0]
            filename = "videos/"+key+os.path.basename(videourl)
            r=requests.get("https://"+videourl)
            with open(filename,"wb") as f:
                 f.write(r.content)
            f.close()
        except Exception:#如果有异常则输出告警 EXception可以捕获python中任意异常，它属于异常基类
           print("可能没有文件下载 !")


    # 启动方法
    def go(self):
        login_name = '13501884996'
        login_password = 'youngmaker22'
        cookies=self.login(login_name,login_password)
        req_cookies =self.sele2req_cookie(cookies)
       #content = requests.get(myspace, headers=headers, cookies=req_cookies)
        htmls = self.__fetch_content(req_cookies,self.my_url)
        anchors = self.__getcourseurl(htmls)
        self.__downloadAll(anchors,req_cookies)

t = CourseSpider()
t.go()

 




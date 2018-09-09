
import threading
import time

class Item(object):
      def __init__(self,name,age,isVisited,isDownload):
          self.name=name
          self.age=age
          self.isVisited=isVisited
          self.isDownload=isDownload

class DictListTest(object):
      
      globalList=[]
      def testList(self):

          addThread=threading.Thread(target=self.addList,args=())
          addThread.start()

          time.sleep(10)

          delThread=threading.Thread(target=self.delList,args=())
          delThread.start()

          dealThread=threading.Thread(target=self.dealList,args=())
          dealThread.start()

      def addList(self):
          for num in range(1,10000):
              item=Item("name"+str(num),num,False,False)
              DictListTest.globalList.append(item)
              print("添加:"+item.name)

      #一边移除元素，一边便利list，不会出错
      def delList(self):
          #tempFilterd=list(DictListTest.globalList)
          while DictListTest.globalList and len(DictListTest.globalList) >0 :
                for item in DictListTest.globalList:
                    print("移除:"+item.name)
                    DictListTest.globalList.remove(item)
          self.dealList()

                #tempFilterd=list(DictListTest.globalList)
 

      #测试改变序列中的对象的值，运行正常
      def dealList(self):
          #tempVisitedFilterd=list(filter(lambda d:d.isVisited==False,DictListTest.globalList))
          while DictListTest.globalList and len(DictListTest.globalList) >0:
                for item in DictListTest.globalList:
                    item.isVisited=True
                    item.isDownload=True
                    print("修改:"+item.name+"为已访问")
                tempVisitedFilterd=list(filter(lambda d:d.isVisited==False,DictListTest.globalList))

          #tempDownloadedFilterd=list(filter(lambda d:d.isDownload==False,DictListTest.globalList))
          #while tempDownloadedFilterd and len(tempDownloadedFilterd) >0:
          #      for item in tempDownloadedFilterd:
          #          item.isDownload=True
          #          print("修改:"+item.name+"为已下载")
          #      tempDownloadedFilterd=list(filter(lambda d:d.isDownload==False,DictListTest.globalList))
          time.sleep(1)
          self.dealList()


test=DictListTest()
test.testList()





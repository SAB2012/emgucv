﻿//----------------------------------------------------------------------------
//  Copyright (C) 2004-2013 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.Util;

namespace Emgu.CV.Features2D
{
   /// <summary>
   /// Descriptor matcher
   /// </summary>
   public abstract class DescriptorMatcher : UnmanagedObject
   {
      /// <summary>
      /// Find the k-nearest match
      /// </summary>
      /// <param name="queryDescriptor">An n x m matrix of descriptors to be query for nearest neighbours. n is the number of descriptor and m is the size of the descriptor</param>
      /// <param name="trainIdx">The resulting n x <paramref name="k"/> matrix of descriptor index from the training descriptors</param>
      /// <param name="distance">The resulting n x <paramref name="k"/> matrix of distance value from the training descriptors</param>
      /// <param name="k">Number of nearest neighbors to search for</param>
      /// <param name="mask">Can be null if not needed. An n x 1 matrix. If 0, the query descriptor in the corresponding row will be ignored.</param>
      public void KnnMatch(IInputArray queryDescriptor, Matrix<int> trainIdx, Matrix<float> distance, int k, IInputArray mask)
      {
         DescriptorMatcherInvoke.CvDescriptorMatcherKnnMatch(Ptr, queryDescriptor.InputArrayPtr, trainIdx, distance, k, mask == null ? IntPtr.Zero : mask.InputArrayPtr);
      }

      /// <summary>
      /// Add the model descriptors
      /// </summary>
      /// <param name="modelDescriptors">The model discriptors</param>
      public void Add(IInputArray modelDescriptors)
      {
         DescriptorMatcherInvoke.CvDescriptorMatcherAdd(_ptr, modelDescriptors.InputArrayPtr);
      }
   }

   internal static partial class DescriptorMatcherInvoke
   {
      static DescriptorMatcherInvoke()
      {
         CvInvoke.CheckLibraryLoaded();
      }

      [DllImport(CvInvoke.EXTERN_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      internal extern static void CvDescriptorMatcherAdd(IntPtr matcher, IntPtr trainDescriptor);

      [DllImport(CvInvoke.EXTERN_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      internal extern static void CvDescriptorMatcherKnnMatch(IntPtr matcher, IntPtr queryDescriptors,
                   IntPtr trainIdx, IntPtr distance, int k,
                   IntPtr mask);
   }
}
